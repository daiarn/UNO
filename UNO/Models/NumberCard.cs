using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    class NumberCard : Card
    {
        private int number { get; set; }

        public NumberCard(string color, int number)
        {
            this.number = number;
            Color = color;
        }
    }
}
