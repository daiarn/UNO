using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO.Strategy
{
    class AllActionCardDeckStrategy : GameModeStrategy
    {
        public Deck getDect(Deck playDeck, Deck usedDeck)
        {
            playDeck.RemoveWildAndNumberCards();
            return playDeck;
        }
    }
}
