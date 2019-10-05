using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Player
    {
        public int playerId { get; set; }
        public string nick { get; set; }
        public List<Card> cards { get; set; }

    }
}
