using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Game
    {
        public List<Player> players { get; set; }
        public Player currentPlayerMove { get; set; }

        public Card topCard { get; set; }
    }
}
