using System;
//using UNO.Strategy;

namespace UNO.Models
{
	enum GamePhase
	{
		WaitingForPlayers, Playing, Finished
	}

	public class Game
    {
		private GamePhase phase;
        public DateTime gameTime { get; set; }

        public bool flowClockWise { get; set; }
        public Deck drawPile { get; set; }
        public Deck discardPile { get; set; }

		public Player[] players;
		public int numPlayers;
		public int activePlayer; // player whose turn it is

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

		public void AddPlayer(string name)
		{
			int index = numPlayers;
			players[index] = new Player(index, name);
			numPlayers++;
		}

		public void RemovePlayer(int index)
		{
			var temp = players[numPlayers];
			temp.id = index;
			players[index] = temp;
			players[numPlayers] = null;
			numPlayers--;
		}

		public int GetActivePlayers()
		{
			int count = 0;
			for (int i = 0; i < numPlayers; i++)
			{
				if (players[i].isPlaying)
					count++;
			}
			return count;
		}

		// other methods

		public Card FromDrawPile() // safely draws from the draw pile following the reset rules
		{
			if (drawPile.GetCount() > 0)
			{
				return drawPile.DrawTopCard();
			}
			else
			{
				if (true) // TODO: infinite draw pile attribute/property/variable
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
    }
}
