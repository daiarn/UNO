using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class ReverseStrategy : ICardStrategy
    {
        public void Action()
        {
            Game.GetInstance().flowClockWise = !Game.GetInstance().flowClockWise;
        }
    }
}