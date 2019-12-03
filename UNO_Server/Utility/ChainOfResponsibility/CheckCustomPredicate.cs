using System;
using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.Utility.ChainOfResponsibility
{
	public class CheckCustomPredicate : ConditionChain
	{
		private readonly Func<Game, bool> predicate;
		private readonly string failMessage;

		public CheckCustomPredicate(Func<Game, bool> predicate, string failMessage)
		{
			this.failMessage = failMessage;
			this.predicate = predicate;
		}

		public override BaseResult ProcessChain(Game game)
		{
			if (!predicate.Invoke(game))
				return new FailResult(failMessage);

			return next.ProcessChain(game);
		}
	}
}
