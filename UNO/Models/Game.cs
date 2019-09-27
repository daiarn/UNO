using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    class Game
    {
        public DateTime gameTime { get; set; }
        public Boolean flowClockWise { get; set; }
        public Deck deck { get; set; }
        public Deck usedDeck { get; set; }

        private static Game instance = null;
        private Game() {}

        public static Game getInstance()
        {
            if (instance == null)
            {
                instance = new Game();
            }

            return instance;
        }
    }
}
