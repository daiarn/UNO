using UNO_Server.Models;

namespace UNO_Server.Utility.Mediator
{
	public class CardCounter : ACounter
	{
		public void Count(Game game)
		{
			game.cardCount++;
		}
	}
}
