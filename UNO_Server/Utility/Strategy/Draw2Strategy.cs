using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw2Strategy : ICardStrategy
	{
		public void Action()
		{
			var game = Game.GetInstance();
			var targetPlayer = game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)];

			game.GivePlayerACard(targetPlayer, game.FromDrawPile());
			game.GivePlayerACard(targetPlayer, game.FromDrawPile());

			game.SkipNextPlayerTurn();
		}
	}
}