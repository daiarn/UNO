using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Iterator
{
    public class MyIterator : IIterator
    {
        IAggregate aggregate_ = null;
        int currentIndex_ = 0;

        public MyIterator(IAggregate aggregate)
        {
            aggregate_ = aggregate;
        }

        public object First
        {
            get
            {
                currentIndex_ = 0;
                return aggregate_[currentIndex_];
            }
        }

        public object Next
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
                    return string.Empty;
                }
            }
        }

        public object Current
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
