using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    class ActionCard : Card
    {
        private string type { get; set; }

        public ActionCard(string color, string action)
        {
            this.type = action;
            Color = color;
        }

    }
}
