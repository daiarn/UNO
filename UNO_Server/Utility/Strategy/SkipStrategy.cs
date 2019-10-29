using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class SkipStrategy : ICardStrategy
	{
		public void Action()
		{
			Game.GetInstance().NextPlayerSkipsTurn();
		}
	}
}
