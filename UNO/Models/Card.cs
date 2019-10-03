using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public class Card
    {
        private string color { get; set; }
        //private String icon { get; set; }

        public Card(string color)
        {
            Color = color;
        }

        public double Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value < 0 ? -value : value;
            }
        }
    }
}
