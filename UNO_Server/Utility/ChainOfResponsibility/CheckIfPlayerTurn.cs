using System;
using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.Utility.ChainOfResponsibility
{
	public class CheckIfPlayerTurn : ConditionChain
	{
		private readonly Guid id;

		public CheckIfPlayerTurn(Guid id)
		{
			this.id = id;
		}

		public override BaseResult ProcessChain(Game game)
		{
			if (game.players[game.activePlayerIndex].id != id)
				return new FailResult("Not your turn");

			return next.ProcessChain(game);
		}
	}
}
