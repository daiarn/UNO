using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public class Card
    {
        public string color { get; set; }
        //private String icon { get; set; }

        public Card(string color)
        {
            Color = color;
        }

    }
}
