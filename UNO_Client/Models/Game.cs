using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Game
    {
        bool success;
        GameState gamestate;

        public bool Success { get => success; set => success = value; }
        public GameState Gamestate { get => gamestate; set => gamestate = value; }
    }
}