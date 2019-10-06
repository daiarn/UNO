using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Game
    {
        List<Player> players;
        Player currentPlayerMove;
        Card topCard;

        public List<Player> Players { get => players; set => players = value; }
        public Player CurrentPlayerMove { get => currentPlayerMove; set => currentPlayerMove = value; }
        public Card TopCard { get => topCard; set => topCard = value; }
    }
}