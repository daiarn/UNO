using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.Utility.ChainOfResponsibility
{
	public class CheckGamePhase : ConditionChain
	{
		private readonly GamePhase phase;

		public CheckGamePhase(GamePhase phase)
		{
			this.phase = phase;
		}

		public override BaseResult ProcessChain(Game game)
		{
			if (game.phase != phase)
				return new FailResult("Wrong game phase");

			return next.ProcessChain(game);
		}
	}
}
