using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace UNO_Server.Utility.Iterator
{
    public interface IIterator
    {
        object First { get; }
        object Next { get; }
        object Current { get; }
        bool HasNext { get; }
    }
}
