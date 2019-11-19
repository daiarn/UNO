using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	class SkipTemplate : BaseTemplate
	{
		public override void PassNextTurn(Game game)
		{
			game.SkipNextPlayerTurn();
		}
	}
}
