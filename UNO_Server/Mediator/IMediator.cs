﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Mediator
{
    public interface IMediator
    {
        void Notify(string ev);
    }
}
