﻿using System;
using UNO_Server.Utility;

namespace UNO.Models
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
		public ExpectedPlayerAction expectedAction;

        private static readonly Game instance = new Game();

        private Game()
        {
			phase = GamePhase.WaitingForPlayers;
			players = new Player[10];

			// something?
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

		// player & card method

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

		// card related methods

		public static bool CanCardBePlayed(Card activeCard, Card playerCard)
		{
			if (activeCard == null) return true; // wait, what? how did this happen? we're smarter than this
			if (playerCard.color == CardColor.Black) return true;

			if (activeCard.color == playerCard.color) return true;
			if (activeCard.type == playerCard.type) return true;

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
				return drawPile.DrawTopCard();
			}
			else
			{
				if (!finiteDeck)
				{
					var activeCard = discardPile.DrawBottomCard();

					drawPile = discardPile;

					discardPile = new Deck();
					discardPile.AddToBottom(activeCard);

					return drawPile.DrawTopCard();
				}
				else
					return null;
			}
		}

		public void PlayCard(Card card)
		{
			throw new NotImplementedException();
		}

		public void DrawCard()
		{
			throw new NotImplementedException();
		}

		// other gameplay methods

		public void StartGame(bool finiteDeck = false, bool onlyNumbers = false)
		{
			phase = GamePhase.Playing;
			activePlayer = 0;

			var builder = new DeckBuilder();
			if (onlyNumbers) builder.setActionCards(0);
			drawPile = builder.build();
			discardPile = new Deck();

			for (int j = 0; j < numPlayers; j++)
			{
				for (int i = 0; i < 7; i++) // each player draws 7 cards
				{
					players[i].hand.Add(FromDrawPile());
				}
			}

			discardPile.AddToBottom(FromDrawPile());
		}
	}
}
