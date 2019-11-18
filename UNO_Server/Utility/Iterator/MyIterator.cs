using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Iterator
{
    public class MyIterator<T> : IIterator<T>
    {
        IAggregate<T> aggregate_ = null;
        int currentIndex_ = 0;

        public MyIterator(IAggregate<T> aggregate)
        {
			currentIndex_ = 0;
            aggregate_ = aggregate;
        }

        public T First
        {
            get
            {
                currentIndex_ = 0;
                return aggregate_[currentIndex_];
            }
        }

        public T Next
        {
            get
            {
                currentIndex_ += 1;

                if (HasNext == false)
                {
                    return aggregate_[currentIndex_];
                }
                else
                {
                    return default(T);
                }
            }
        }

        public T Current
        {
            get
            {
                return aggregate_[currentIndex_];
            }
        }

        public bool HasNext
        {
            get
            {
                if (currentIndex_ < aggregate_.Count)
                {
                    return true;
                }
                return false;
            }
        }
        
    }
}
