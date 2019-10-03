using System;
//using UNO.Strategy;

namespace UNO.Models
{
	public class Game
    {
        public DateTime gameTime { get; set; }
        public Boolean flowClockWise { get; set; }
        public Deck drawPile { get; set; }
        public Deck discardPile { get; set; }
      
      
        private static Game instance = new Game();
      
        private Game()
        {
			// something
        }

        public static Game GetInstance()
        {
            return instance;
        }

		public Card FromDrawPile() // safely draws from the draw pile following the reset rules
		{
			if (drawPile.getCount() > 0)
			{
				return drawPile.drawTopCard();
			}
			else
			{
				if (true) // infinite draw pile mode
				{
					var activeCard = discardPile.drawBottomCard();

					drawPile = discardPile;

					discardPile = new Deck();
					discardPile.addToBottom(activeCard);

					return drawPile.drawTopCard();
				}
				else
					return null;
			}
		}
    }
}
