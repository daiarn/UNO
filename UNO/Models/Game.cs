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
        public Deck deck { get; set; }
        public Deck usedDeck { get; set; }
      
        private readonly Observer[] observers;
        private int observersCount;
      
        public GameModeStrategy strategy { get; set; }
      
        private static Game instance = null;
      
        private Game(GameModeStrategy strategy)
        {
            this.strategy = strategy;
            observers = new Observer[10];
            observersCount = 0;
        }

        public static Game getInstance(GameModeStrategy strategy = null)
        {
            if (instance == null)
            {
                instance = new Game(strategy);
            }

            return instance;
        }

        public Deck getDeck()
        {
            return strategy.getDect(this.deck, this.usedDeck);
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
    }
}
