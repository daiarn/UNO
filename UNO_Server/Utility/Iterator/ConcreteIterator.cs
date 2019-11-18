using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Iterator
{
    public class ConcreteIterator<T> : IIterator<T>
    {
        IAggregate<T> aggregate_ = null;
        int currentIndex_ = 0;

        public ConcreteIterator(IAggregate<T> aggregate)
        {
			currentIndex_ = 0;
            aggregate_ = aggregate;
        }

        public T First()
        {
            currentIndex_ = 0;
            return aggregate_ [currentIndex_];
        }

        public T Next()
        {
            currentIndex_ += 1;

            if (HasNext())
            {
                return aggregate_[currentIndex_];
            }
            else
            {
                return default(T);
            }
        }

        public T Current()
        {
            return aggregate_[currentIndex_];
        }

        public bool HasNext()
        {
            if (currentIndex_ < aggregate_.Count())
            {
                return true;
            }
            return false;
        }

    }
}
