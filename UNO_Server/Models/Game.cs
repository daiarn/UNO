using System;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models.SendData;
using UNO_Server.Utility.BuilderFacade;
using UNO_Server.Utility.Template;

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

		public int activePlayerIndex;

		public GameWatcher gameWatcher; // observers inside
		public CardsCounter cardsCounter;

		private static Game instance = new Game();
		private static readonly ActionFactory cardActionFactory = new ActionFactory();
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
					.SetAllNumberCards(2)
					.SetIndividualNumberCards(0, 1)
				.action
					.SetActionCards(2)
				.wild
					.SetBlackCards(4)
				.Build();

			semiPerfectDeck = new DeckBuilderFacade()
				.number
					.SetAllNumberCards(2)
					.SetIndividualNumberCards(0, 1)
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

		public void EliminatePlayer(Guid id)
		{
			var index = -1;
			for (int i = 0; i < numPlayers; i++)
				if (players[i].id == id) index = i;
			if (index < 0 || index > numPlayers) return;

			if (phase != GamePhase.Playing)
			{
				var last = players[numPlayers - 1];
				players[index] = last;
				players[numPlayers - 1] = null;
				numPlayers--;
			}
			else
			{
				PlayerLoses(index);

				if (index == activePlayerIndex)
					NextPlayerTurn();

				if (GetActivePlayerCount() < 2)
					GameOver();
			}
		}

		public void PlayerLoses(int index)
		{
			players[index].isPlaying = false;
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
			if (drawPile.Count() > 0)
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
			for (var o = iterator.First(); iterator.HasNext(); o = iterator.Next())
			{
				o.Notify(card);
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

			BaseTemplate action = cardActionFactory.Create(card.type);
			//if (action != null)
			action.ProcessAction(this); // action card is responsible whose turn is next
										//else
										//	NextPlayerTurn();
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

		public void StartGame(bool finiteDeck = false, bool onlyNumbers = false, bool slowGame = false)
		{
			phase = GamePhase.Playing;
			this.finiteDeck = finiteDeck;
			// this.slowGame = slowGame;

			flowClockWise = true;
			discardPile = new Deck();
			if (onlyNumbers) drawPile = semiPerfectDeck.MakeDeepCopy();
			else drawPile = perfectDeck.MakeDeepCopy();
			drawPile.Shuffle();

			cardsCounter = new CardsCounter(players);

			activePlayerIndex = 0;

			for (int i = 0; i < numPlayers; i++)
			{
				for (int j = 0; j < 7; j++) // each player draws 7 cards
				{
					GivePlayerACard(players[i], FromDrawPile());
				}
				players[i].isPlaying = true;
			}

			Card firstCard = null;

			for (int i = 0; i < drawPile.Count(); i++)
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
        
        public void PlayerWins(int index)
        {
            phase = GamePhase.Finished;

            // TODO: save winner ;)
        }

        public void GameOver()
		{
			phase = GamePhase.Finished;

			var stillPlaying = players.Where(p => p != null && p.isPlaying)
			.OrderBy(p => p.hand.Aggregate(0, (sum, next) => sum + next.GetScore()))
            .ThenBy(p => (Array.IndexOf(players, p) - activePlayerIndex + numPlayers) % numPlayers)
            .First();


            // TODO: save winner ;)

			//Task.Run(async () =>
			//{
			//	await Task.Delay(10000);
			//	ResetGame();
			//});
		}
	}
}
