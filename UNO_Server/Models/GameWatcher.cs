using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Utility;
using UNO_Server.Utility.Iterator;

namespace UNO_Server.Models
{
    public class GameWatcher : IAggregate
    {
        public Observer[] observers;
        public GameWatcher()
        {
            observers = new Observer[2]
            {
                new ZeroCounter(),
                new WildCounter()
            };

            foreach (var item in observers)
                item.Counter = 0;
        }

        public object this[int itemIndex]
        {
            get => observers[itemIndex];
            set => observers[itemIndex] = (Observer)value;
        }

        public int Count => observers.Length;

        public IIterator GetIterator()
        {
            return new MyIterator(this);
        }
    }
}
