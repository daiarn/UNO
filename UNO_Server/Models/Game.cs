using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Strategy;

namespace UNO.Models
{
    public class Game
    {
        public DateTime gameTime { get; set; }
        public Boolean flowClockWise { get; set; }
        public Deck drawPile { get; set; }
        public Deck discardPile { get; set; }
      
        private readonly Observer[] observers;
        private int observersCount;
      
        public GameModeStrategy Strategy { get; set; }
      
        private static Game instance = null;
      
        private Game(GameModeStrategy strategy)
        {
            this.Strategy = strategy;
            observers = new Observer[10];
            observersCount = 0;
        }

        public static Game GetInstance(GameModeStrategy strategy = null)
        {
            if (instance == null)
            {
                instance = new Game(strategy);
            }

            return instance;
        }

        public Deck GetDeck()
        {
            return Strategy.getDect(this.Deck, this.UsedDeck);
        }
      
        public void AttachObserver(Observer observer)
        {
            try
            {
                observers[observersCount++] = observer;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Game can be played ony with less or equal to 10 players");
            }
        }
        public void NotifyAllObservers(Card card)
        {
            for (int i = 0; i < observersCount; i++)
            {
                observers[i].Update(card);
            }
        }

		public Card fromDrawPile() // safely draws from the draw pile following the reset rules
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
