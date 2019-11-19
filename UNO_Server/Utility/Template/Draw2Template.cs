using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	class Draw2Template : BaseTemplate
	{
		public override void AlterState(Game game)
		{
			var targetPlayer = game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)];

			game.GivePlayerACard(targetPlayer, game.FromDrawPile());
			game.GivePlayerACard(targetPlayer, game.FromDrawPile());
		}

		public override void PassNextTurn(Game game)
		{
			game.SkipNextPlayerTurn();
		}
	}
}