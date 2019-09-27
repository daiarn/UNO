using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO.Strategy
{
    class MultipleDeckStrategy: GameModeStrategy
    {
        public Deck getDect(Deck playDeck, Deck usedDeck)
        {
            if(playDeck.cards.Length == 0)
            {
                playDeck.cards = usedDeck.cards;
            }
            
            return playDeck;
        }
    }
}
