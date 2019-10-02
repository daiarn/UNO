using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    class WildCard : ColorChangeCard
    {
        private string wildType;
        public WildCard(string col, string wild)
        {
            this.wildType = wild;
            color = col;
        }
    }
}
