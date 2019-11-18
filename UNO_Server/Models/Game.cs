using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models.SendData;
using UNO_Server.Utility;
using UNO_Server.Utility.BuilderFacade;
using UNO_Server.Utility.Strategy;

namespace UNO_Server.Models
{
	public enum GamePhase
	{
		WaitingForPlayers, Playing, Finished
	}

	public class Game
	{
		public GamePhase phase;
		public bool finiteDeck = false;

		public bool flowClockWise { get; set; }
		public Deck drawPile { get; set; }
		public Deck discardPile { get; set; }

		public Player[] players;
		public int numPlayers;
		public WinnerInfo[] winners;

		public int activePlayerIndex;
        
        public GameWatcher gameWatcher;//Observers inside
        public CardsCounter cardsCounter;

		private static Game instance = new Game();
		private static readonly StrategyFactory cardActionFactory = new CardActionFactory();
		private readonly Deck perfectDeck;
		private readonly Deck semiPerfectDeck;
		private Game()
		{
			phase = GamePhase.WaitingForPlayers;
			flowClockWise = true;

			discardPile = new Deck();
			drawPile = new Deck();

			players = new Player[10];
			numPlayers = 0;

            gameWatcher = new GameWatcher();

			perfectDeck = new DeckBuilderFacade()
				.number
					.AddNonZeroNumberCards(2)
					.AddIndividualNumberCards(0, 1)
				.action
					.AddActionCards(2)
				.wild
					.AddBlackCards(4)
				.Build();

			semiPerfectDeck = new DeckBuilderFacade()
				.number
					.AddNonZeroNumberCards(2)
					.AddIndividualNumberCards(0, 1)
				.Build();
		}

		public static Game ResetGame()
		{
			instance = new Game();
			return instance;
		}

		public static Game GetInstance()
		{
			return instance;
		}

		public Guid AddPlayer(string name)
		{
			int index = numPlayers;
			players[index] = new Player(name);
			numPlayers++;
			return players[index].id;
		}

		public void DeletePlayer(Guid id)
		{
			var index = -1;
			for (int i = 0; i < numPlayers; i++)
				if (players[i].id == id) index = i;
			if (index < 0 || index > numPlayers) return;

			var last = players[numPlayers - 1];
			players[index] = last;
			players[numPlayers - 1] = null;
			numPlayers--;
		}

		public void EliminatePlayer(Guid id)
		{
			var index = -1;
			for (int i = 0; i < numPlayers; i++)
				if (players[i].id == id) index = i;
			if (index < 0 || index > numPlayers) return;

			PlayerLoses(index);

			if (index == activePlayerIndex)
				NextPlayerTurn();

			if (GetActivePlayerCount() < 2)
				GameOver();
		}

		public void PlayerWins(int index)
		{
			for (int i = 0; i < numPlayers; i++)
				if (winners[i] == null)
				{
					players[index].isPlaying = false;
					winners[i] = new WinnerInfo(index);
					break;
				}
		}

		public void PlayerLoses(int index)
		{
			for (int i = numPlayers - 1; i >= 0; i--)
				if (winners[i] == null)
				{
					players[index].isPlaying = false;
					winners[i] = new WinnerInfo(index);
					break;
				}
		}

		public Player GetPlayerByUUID(Guid id)
		{
			foreach (var player in players)
				if (player != null && player.id == id) return player;
			return null;
		}

		public int GetActivePlayerCount()
		{
			int count = 0;
			for (int i = 0; i < numPlayers; i++)
			{
				if (phase != GamePhase.Playing || players[i].isPlaying)
					count++;
			}
			return count;
		}

		public bool CanPlayerPlayAnyOn(Player player)
		{
			for (int i = 0; i < player.hand.Count; i++)
			{
				var playerCard = player.hand[i];
				if (CanCardBePlayed(playerCard)) return true;
			}
			return false;
		}

		public bool CanCardBePlayed(Card playerCard)
		{
			var activeCard = discardPile.PeekBottomCard();

			if (activeCard == null) return true; // wait, what? how did this happen? we're smarter than this
			if (activeCard.color == playerCard.color) return true;
			if (activeCard.type == playerCard.type) return true;

			if (playerCard.type == CardType.Wild || playerCard.type == CardType.Draw4) return true;
			return false;
		}

		public void GivePlayerACard(Player player, Card card)
		{
			player.hand.Add(card);
			cardsCounter.AddCard(player.id, 1);
		}

		public Card FromDrawPile() // draws from the draw pile according to the deck finite-ness rules
		{
			if (drawPile.GetCount() > 0)
			{
                var card = drawPile.DrawTopCard();
				NotifyAllObservers(card);
				return card;
			}
			else
			{
				if (!finiteDeck)
				{
                    var activeCard = discardPile.DrawBottomCard();

					drawPile = discardPile;

					discardPile = new Deck();
					discardPile.AddToBottom(activeCard);

					var card = drawPile.DrawTopCard();
					if (card != null)
						NotifyAllObservers(card);

					return card;
				}
				else
					return null;
			}
		}

		private void NotifyAllObservers(Card card)
		{
            var iterator = gameWatcher.GetIterator();
            while (iterator.HasNext)
            {
                Observer observer = (Observer)iterator.Next;
                observer.Notify(card);
            }
		}

		public void PlayerDrawsCard()
		{
			var player = players[activePlayerIndex];
			var card = FromDrawPile();

			if (card == null)
				GameOver();
			else
			{
				GivePlayerACard(player, card);

				if (!CanCardBePlayed(card))
					NextPlayerTurn();
			}
		}

		public void PlayerPlaysCard(Card card)
		{
			var player = players[activePlayerIndex];

			player.hand.Remove(card);
			discardPile.AddToBottom(card);

			if (player.hand.Count == 0) // winner
			{
				PlayerWins(activePlayerIndex);
				if (GetActivePlayerCount() < 2)
				{
					GameOver();
					return;
				}
			}

			bool playerSaidUNO = true; // TODO: check player UNO penalty
			if (!playerSaidUNO && player.hand.Count == 1)
			{
				GivePlayerACard(player, FromDrawPile());
				GivePlayerACard(player, FromDrawPile());
			}
			/*
			else if (playerSaidUNO)
			{
				GivePlayerACard(player, FromDrawPile());
				GivePlayerACard(player, FromDrawPile());
			}//*/

			ICardStrategy action = cardActionFactory.CreateAction(card.type);
			if (action != null)
				action.Action(); // action card is responsible whose turn is next
			else
				NextPlayerTurn();
		}

		public void PlayerSaysUNO()
		{
			//var player = players[activePlayerIndex];

			// TODO: avoid UNO penalty here
		}

		public int GetNextPlayerIndexAfter(int playerIndex)
		{
			int nextPlayerIndex = playerIndex;
			do
			{
				if (flowClockWise)
				{
					nextPlayerIndex++;
					if (nextPlayerIndex >= numPlayers)
						nextPlayerIndex = 0;
				}
				else
				{
					nextPlayerIndex--;
					if (nextPlayerIndex < 0)
						nextPlayerIndex = numPlayers - 1;
				}
			} while (!players[nextPlayerIndex].isPlaying && nextPlayerIndex != playerIndex);

			if (nextPlayerIndex == playerIndex && GetActivePlayerCount() > 2)
				throw new NotImplementedException("Failed to get next player"); // if you are reading this, then you did something wrong, because you would have gotten an infinite loop

			return nextPlayerIndex;
		}

		public void SkipNextPlayerTurn()
		{
			activePlayerIndex = GetNextPlayerIndexAfter(GetNextPlayerIndexAfter(activePlayerIndex));
		}

		public void NextPlayerTurn()
		{
			activePlayerIndex = GetNextPlayerIndexAfter(activePlayerIndex);
		}

		public void ReverseFlow()
		{
			flowClockWise = !flowClockWise;
		}

		public void StartGame(bool finiteDeck = false, bool onlyNumbers = false)
		{
			phase = GamePhase.Playing;
			this.finiteDeck = finiteDeck;

			flowClockWise = true;
			discardPile = new Deck();
			if (onlyNumbers) drawPile = semiPerfectDeck.MakeDeepCopy();
			else drawPile = perfectDeck.MakeDeepCopy();
			drawPile.Shuffle();

            cardsCounter = new CardsCounter(players, numPlayers);

			activePlayerIndex = 0;

			winners = new WinnerInfo[numPlayers];

			for (int i = 0; i < numPlayers; i++)
			{
				for (int j = 0; j < 7; j++) // each player draws 7 cards
				{
					GivePlayerACard(players[i], FromDrawPile());
				}
				players[i].isPlaying = true;
			}

			Card firstCard = null;

			for (int i = 0; i < drawPile.GetCount(); i++)
			{
				var aCard = drawPile.DrawTopCard();
				if (Card.numberCardTypes.Contains(aCard.type))
				{
					firstCard = aCard;
					break;
				}
				drawPile.AddToBottom(aCard);
			}

			if (firstCard == null) // panic mode
				throw new NotImplementedException("Discard pile doesn't contain valid starting cards");

			discardPile.AddToBottom(firstCard);
			NextPlayerTurn();
		}

		public void GameOver()
		{
			phase = GamePhase.Finished;

			var stillPlaying = players.Where(p => p != null && p.isPlaying).Select(
				p => new WinnerInfo(Array.IndexOf(players, p), p.hand, (Array.IndexOf(players, p) - activePlayerIndex) % numPlayers))
			.OrderByDescending(p => p.score).ThenBy(p => p.turn);

			int start;
			for (start = 0; start < numPlayers; start++)
				if (winners[start] != null) break;

			foreach (var item in stillPlaying)
				winners[start++] = item;

			Task.Factory.StartNew(() => { System.Threading.Thread.Sleep(10000); ResetGame(); });
		}
	}
}
