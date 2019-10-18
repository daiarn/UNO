using System;

namespace UNO.Models
{
    class SkipStrategy : ICardStrategy
    {
        public void Action()
        {
            Game.GetInstance().SkipAction();
        }
    }
}
