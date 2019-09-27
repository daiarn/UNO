using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO.Strategy
{
    interface GameModeStrategy
    {
        Deck getDect(Deck playDeck, Deck usedDeck);
    }
}
