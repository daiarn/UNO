using System;
using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.ChainOfResponsibility
{
	public class CheckIfPlayerExists : ConditionChain
	{
		private Guid id;

		public CheckIfPlayerExists(Guid id)
		{
			this.id = id;
		}

		public override BaseResult ProcessChain(Game game)
		{
			var player = game.GetPlayerByUUID(id);
			if (player == null)
				return new FailResult("You are not in the game");

			return next.ProcessChain(game);
		}
	}
}
