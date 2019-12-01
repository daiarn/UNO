using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Mediator
{
    public class ConcreteMediator : IMediator
    {
        private CardCounter cardCounter;
        private MoveCounter moveCounter;
        private SkipCounter skipCounter;

        public ConcreteMediator(CardCounter cardCounter, MoveCounter moveCounter, SkipCounter skipCounter)
        {
            this.skipCounter = skipCounter;
            this.cardCounter = cardCounter;
            this.moveCounter = moveCounter;
        }

        public void Notify(object sender, string ev)
        {
            
        }
    }
}
