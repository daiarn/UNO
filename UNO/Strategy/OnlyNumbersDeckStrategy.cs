using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO.Strategy
{
    class OnlyNumbersDeckStrategy : GameModeStrategy
    {
        public Deck getDect(Deck playDeck, Deck usedDeck)
        {
            playDeck.RemoveActionAndWildCards();
            return playDeck;
        }
    }
}
