﻿using System;
using System.Collections.Generic;
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

	public enum ExpectedPlayerAction
	{
		DrawCard, PlayCard, SayUNO
	}

	public class Game
	{
		public GamePhase phase;
		public DateTime gameTime { get; set; }
		public bool finiteDeck = false;

		public bool flowClockWise { get; set; }
		public Deck drawPile { get; set; }
		public Deck discardPile { get; set; }

		public Player[] players;
		public int numPlayers;

		public ExpectedPlayerAction expectedAction;
		public int activePlayerIndex;

		public List<Observer> observers;

		private static Game instance = new Game();
		private static readonly Factory cardActionFactory = new CardActionFactory();
		private readonly Deck perfectDeck;
		private readonly Deck semiPerfectDeck;
		private Game()
		{
			phase = GamePhase.WaitingForPlayers;
			flowClockWise = true;

			observers = new List<Observer>
			{
				new ZeroCounter(),
				new WildCounter()
			};

			NewGamePrep();

			perfectDeck = new DeckBuilderFacade()
				.number
					.addNonZeroNumberCards(2)
					.addIndividualNumberCards(0, 1)
				.action
					.addActionCards(2)
				.wild
					.addBlackCards(4)
				.build();

			semiPerfectDeck = new DeckBuilderFacade()
				.number
					.addNonZeroNumberCards(2)
					.addIndividualNumberCards(0, 1)
				.build();
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

		// player methods
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

			var player = players[index];
			player.isPlaying = false;

			// TODO: check if it's that player's turn (also check if it's game over)
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

		// card methods

		public bool CanPlayerPlayAnyOn(Player player)
		{
			var activeCard = discardPile.PeekBottomCard();
			for (int i = 0; i < player.hand.Count; i++)
			{
				var playerCard = player.hand[i];
				if (CanCardBePlayed(activeCard, playerCard)) return true;
			}
			return false;
		}

		public static bool CanCardBePlayed(Card activeCard, Card playerCard)
		{
			if (activeCard == null) return true; // wait, what? how did this happen? we're smarter than this
			if (activeCard.color == playerCard.color) return true;
			if (activeCard.type == playerCard.type) return true;

			if (playerCard.type == CardType.Wild || playerCard.type == CardType.Draw4) return true;
			return false;
		}

		public bool CanCardBePlayed(Card playerCard)
		{
			return CanCardBePlayed(discardPile.PeekBottomCard(), playerCard);
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
			foreach (var item in observers)
			{
				item.Notify(card);
			}
		}

		// player action methods

		public void PlayerDrawsCard()
		{
			var player = players[activePlayerIndex];
			var card = FromDrawPile();

			if (card == null)
				GameOver();
			else
				player.hand.Add(card);

			if (CanCardBePlayed(card))
				expectedAction = ExpectedPlayerAction.PlayCard;
			else
				NextPlayerTurn();
		}

		public void PlayerPlaysCard(Card card)
		{
			var player = players[activePlayerIndex];

			player.hand.Remove(card);
			discardPile.AddToBottom(card);

			// TODO: check if player needs to say UNO

			// TODO: check if player has won
			//GameOver();

			ICardStrategy action = cardActionFactory.CreateAction(card.type);
			if (action != null)
				action.Action();
			else
				NextPlayerTurn();

		}

		public void PlayerSaysUNO()
		{
			//var player = players[activePlayerIndex];

			// TODO: avoid card draw penalty

			NextPlayerTurn();
		}

		// player turn logic

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
				throw new NotImplementedException("Failed to get next player");// if you are reading this, then you did something wrong, because you would have gotten an infinite loop

			return nextPlayerIndex;
		}

		public void SkipNextPlayerTurn()
		{
			activePlayerIndex = GetNextPlayerIndexAfter(activePlayerIndex);
			NextPlayerTurn();
		}

		public void NextPlayerTurn()
		{
			//int nextNextPlayer = GetNextPlayerIndexAfter(nextPlayerIndex);
			//activePlayerIndex = nextPlayerIndex;
			//nextPlayerIndex = nextNextPlayer;
			activePlayerIndex = GetNextPlayerIndexAfter(activePlayerIndex);

			Player player = players[activePlayerIndex];
			if (CanPlayerPlayAnyOn(player))
				expectedAction = ExpectedPlayerAction.PlayCard;
			else
				expectedAction = ExpectedPlayerAction.DrawCard;

		}

		public void ReverseFlow()
		{
			flowClockWise = !flowClockWise;
		}

		// game progress methods

		public void NewGamePrep()
		{
			phase = GamePhase.WaitingForPlayers;

			discardPile = new Deck();
			drawPile = new Deck();

			players = new Player[10];
			numPlayers = 0;

			foreach (var item in observers)
				item.Counter = 0;
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

			activePlayerIndex = 0;
			//nextPlayerIndex = 0;

			for (int i = 0; i < numPlayers; i++)
			{
				for (int j = 0; j < 7; j++) // each player draws 7 cards
				{
					players[i].hand.Add(FromDrawPile());
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

			throw new NotImplementedException("Game over, go home");
		}

		public void UndoDrawCard(int playerIndex)
		{
			if (playerIndex != -1)
			{
				int index = players[playerIndex].hand.Count - 1;
				Card card = players[playerIndex].hand[index];
				players[playerIndex].hand.Remove(card);
				drawPile.AddtoTop(card);
			}
		}

		public void UndoUno()
		{
			//do nothing for now
		}
	}
}
