using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Player
    {
        int playerId;
        string nick;
        List<Card> cards;
        public List<Card> Cards { get => cards; set => cards = value; }
        public string Nick { get => nick; set => nick = value; }
        public int PlayerId { get => playerId; set => playerId = value; }
    }
}
