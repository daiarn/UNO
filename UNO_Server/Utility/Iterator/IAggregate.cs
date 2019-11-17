using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Iterator
{
    public interface IAggregate
    {
        IIterator GetIterator();
        object this[int itemIndex] { set; get; }
        int Count { get; }
    }
}
