using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models;

namespace UNO_Server.Mediator
{
    public class CardCounter : ACounter
    {
        public void Count(Game game)
        {
            game.cardCount++;
        }
    }
}
