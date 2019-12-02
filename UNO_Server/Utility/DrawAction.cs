using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models;
using UNO_Server.Models.SendData;

namespace UNO_Server.Utility
{
    public class DrawAction
    {
        public GamePhase phase;
        public bool flowClockWise { get; set; }
        public Deck drawPile { get; set; }
        public Deck discardPile { get; set; }

        public Player[] players;
        public int numPlayers;

        public int activePlayerIndex;
    }
}
