using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Strategy;

namespace UNO.Models
{
    class Game
    {
        public DateTime gameTime { get; set; }
        public Boolean flowClockWise { get; set; }
        public Deck deck { get; set; }
        public Deck usedDeck { get; set; }

        public GameModeStrategy strategy { get; set; }

        private static Game instance = null;
        private Game(GameModeStrategy strategy)
        {
            this.strategy = strategy;
        }

        public static Game getInstance(GameModeStrategy strategy = null)
        {
            if (instance == null)
            {
                instance = new Game(strategy);
            }

            return instance;
        }

        public Deck getDeck()
        {
            return strategy.getDect(this.deck, this.usedDeck);
        }
    }
}
