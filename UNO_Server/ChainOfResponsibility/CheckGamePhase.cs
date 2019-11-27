using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.ChainOfResponsibility
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
				return new FailResult(string.Format("You can only do this in phase {0}", phase.ToString()));

			return next.ProcessChain(game);
		}
	}
}
