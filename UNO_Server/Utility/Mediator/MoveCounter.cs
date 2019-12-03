using UNO_Server.Models;

namespace UNO_Server.Utility.Mediator
{
	public class MoveCounter : ACounter
	{
		public void Count(Game game)
		{
			game.moveCount++;
		}
	}
}
