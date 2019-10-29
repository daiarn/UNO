using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	public class WildStrategy : ICardStrategy
	{
		public void Action()
		{
			Game.GetInstance().NextPlayerTurn(); // this action card can be removed
		}
	}
}
