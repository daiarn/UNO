using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	public class BaseTemplate
	{
		public void ProcessAction(Game game)
		{
			AlterState(game);
			PassNextTurn(game);
		}

		public virtual void AlterState(Game game) { }
		public virtual void PassNextTurn(Game game)
		{
			game.NextPlayerTurn();
		}
	}
}
