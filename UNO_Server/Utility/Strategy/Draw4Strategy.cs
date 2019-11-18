using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw4Strategy : ICardStrategy
	{
		public void Action()
		{
			var game = Game.GetInstance();
			var targetPlayer = game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)];

			for (int i = 0; i < 4; i++)
				game.GivePlayerACard(targetPlayer, game.FromDrawPile());

			game.SkipNextPlayerTurn();
		}
	}
}