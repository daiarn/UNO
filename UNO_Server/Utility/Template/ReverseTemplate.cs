using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	class ReverseTemplate : BaseTemplate
	{
		public override void AlterState(Game game)
		{
			if (game.GetActivePlayerCount() > 2)
				game.ReverseFlow();
		}

		public override void PassNextTurn(Game game)
		{
			if (game.GetActivePlayerCount() > 2)
				game.NextPlayerTurn();
			else
				game.SkipNextPlayerTurn();
		}
	}
}