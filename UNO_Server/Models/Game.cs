﻿using System;
using System.Collections.Generic;
using UNO_Server.Utility;
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

		public int activePlayer; // player whose turn it is

        public Player nextPlayer;
		public ExpectedPlayerAction expectedAction;
		public List<Observer> observers;

        private static readonly Game instance = new Game();
		private static readonly Factory cardActionFactory = new CardActionFactory();
		private Game()
        {
			phase = GamePhase.WaitingForPlayers;
			players = new Player[10];
			numPlayers = 0;

			discardPile = new Deck();
			drawPile = new Deck();

			observers = new List<Observer>
			{
				new ZeroCounter(),
				new WildCounter()
			};
			// more?
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

		private int GetPlayerIdByUUID(Guid id)
		{
			for (int i = 0; i < players.Length; i++)
			{
				if (players[i].id == id) return i;
			}
			return -1;
		}

		public void DeletePlayer(Guid id)
		{
			var index = GetPlayerIdByUUID(id);
			if (index < 0 || index > numPlayers) return;

			var temp = players[numPlayers];
			players[index] = temp;
			players[numPlayers] = null;
			numPlayers--;
		}

		public void EliminatePlayer(Guid id)
		{
			var index = GetPlayerIdByUUID(id);
			if (index < 0 || index > numPlayers) return;

			var player = players[numPlayers];
			player.isPlaying = false;
			// TODO: check if it's that player's turn (also check if it's game over)
		}

		public Player GetPlayerById(Guid id)
		{
			foreach (var player in players)
				if (player != null && player.id == id) return player;
			return null;
		}

		public int GetActivePlayers()
		{
			int count = 0;
			for (int i = 0; i < numPlayers; i++)
			{
				if (phase != GamePhase.Playing || players[i].isPlaying)
					count++;
			}
			return count;
		}

		// player & card methods

		public bool CanPlayerPlayAnyOn(Player player)
		{
			var activeCard = discardPile.PeekBottomCard();
			for (int i = 0; i < player.hand.Count(); i++)
			{
				var playerCard = player.hand.GetCard(i);
				if (CanCardBePlayed(activeCard, playerCard)) return true;
			}
			return false;
		}

		public static bool CanCardBePlayed(Card activeCard, Card playerCard)
		{
			//if (activeCard == null) return true; // wait, what? how did this happen? we're smarter than this
			if (activeCard.color == playerCard.color) return true;
			if (activeCard.type == playerCard.type) return true;

			if (playerCard.type == CardType.Wild || playerCard.type == CardType.Draw4) return true;
			return false;
		}

		public bool CanCardBePlayed(Card playerCard)
		{
			return Game.CanCardBePlayed(discardPile.PeekBottomCard(), playerCard);
		}

		public Card FromDrawPile() // safely draws from the draw pile following the reset rules
		{
			if (drawPile.GetCount() > 0)
			{
				var card = drawPile.DrawTopCard();
				NotifyAll(card);
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
					NotifyAll(card);
					return card;
				}
				else
					return null;
			}
		}

		private void NotifyAll(Card card)
		{
			foreach (var item in observers)
			{
				item.Notify(card);
			}
		}

		public void PlayCard(Player player, Card card)
		{
			player.hand.Remove(card);
			discardPile.AddToBottom(card);

			ICardStrategy action = cardActionFactory.CreateAction(card.type);
			if (action != null) action.Action();
		}

        public void SkipAction()
        {
            throw new NotImplementedException();
        }

		public void DrawCard(Player player)
		{
			var card = FromDrawPile();
			if (card == null)
				GameOver();
			else
				player.hand.Add(card);

			// TODO: give turn to next player (if card cannot be played)
		}

		// other gameplay methods

		public void StartGame(bool finiteDeck = false, bool onlyNumbers = false)
		{
			this.finiteDeck = finiteDeck;
			phase = GamePhase.Playing;
			activePlayer = 0;

			var builder = new DeckBuilder();
			if (onlyNumbers) builder.setActionCards(0);
			drawPile = builder.build();
			discardPile = new Deck();

			//drawPile.Shuffle();

			for (int i = 0; i < numPlayers; i++)
			{
				for (int j = 0; j < 7; j++) // each player draws 7 cards
				{
					players[i].hand.Add(FromDrawPile());
				}
			}

			discardPile.AddToBottom(FromDrawPile()); // TODO: check what card
		}

		public void GameOver()
		{
			throw new NotImplementedException();
		}
	}
}
