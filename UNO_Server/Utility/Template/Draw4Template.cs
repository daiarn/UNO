using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	class Draw4Template : BaseTemplate
	{
		public override void AlterState(Game game)
		{
			var targetPlayer = game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)];
			for (int i = 0; i < 4; i++)
				game.GivePlayerACard(targetPlayer, game.FromDrawPile());
		}

		public override void PassNextTurn(Game game)
		{
			game.SkipNextPlayerTurn();
		}
	}
}