using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Utility.Iterator;

namespace UNO_Server.Models
{
    public class CardsCounter : IAggregate<int>
	{
        private Dictionary<Guid, int> PlayerCarsCounter;


        public CardsCounter(Player[] players, int playerCounter)
        {
            PlayerCarsCounter = new Dictionary<Guid, int>();
            for (int i = 0; i < playerCounter; i++)
            {
                PlayerCarsCounter.Add(players[i].id, 0);
            }
        }
        public void AddCard(Guid index, int count)
        {
            PlayerCarsCounter[index] += count;
        }
        public int this[int itemIndex]
        {
            get
            {
                if (itemIndex < PlayerCarsCounter.Count)
                {
                    return PlayerCarsCounter.Values.ToList()[itemIndex];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                PlayerCarsCounter.Values.ToList()[itemIndex] += (int)value;
            }
        }

        public int Count()
        {
            return PlayerCarsCounter.Count;
        }

        public IIterator<int> GetIterator()
        {
            return new ConcreteIterator<int>(this);
        }
    }
}
