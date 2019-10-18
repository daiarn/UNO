using System;

namespace UNO.Models
{
    class ReverseStrategy : ICardStrategy
    {
        public void Action()
        {
            Game.GetInstance().flowClockWise = !Game.GetInstance().flowClockWise;
        }
    }
}