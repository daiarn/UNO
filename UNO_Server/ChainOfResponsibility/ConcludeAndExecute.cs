using System;
using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.ChainOfResponsibility
{
	public class ConcludeAndExecute : ConditionChain
	{
		private readonly Func<Game, BaseResult> action;

		public ConcludeAndExecute(Func<Game, BaseResult> action)
		{
			this.action = action;
		}

		public override BaseResult ProcessChain(Game game)
		{
			return action.Invoke(game);
		}
	}
}
