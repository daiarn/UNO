using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public abstract class Observer
    {
        protected Game Game;
        public abstract void Update(Card card);
    }
}
