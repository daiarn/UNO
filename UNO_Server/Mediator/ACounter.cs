using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Mediator
{
    public abstract class ACounter
    {
        protected IMediator mediator;

        public ACounter(IMediator mediator = null)
        {
            this.mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }

    }
}
