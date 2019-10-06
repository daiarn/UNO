using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Game
    {
        private List<Player> _players;
        private Player _currentPlayerMove;
        private Card _topCard;

        public List<Player> Players { get => _players; set => _players = value; }
        public Player CurrentPlayerMove { get => _currentPlayerMove; set => _currentPlayerMove = value; }

        public Card TopCard { get => _topCard; set => _topCard = value; }
    }
}
