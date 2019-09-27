using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public class Game
    {
        private DateTime gameTime { get; set; }
        private Boolean flowClockWise { get; set; }
        private Deck deck { get; set; }
        private readonly Observer[] observers;
        private int observersCount;

        public Game()
        {
            observers = new Observer[10];
            observersCount = 0;
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
