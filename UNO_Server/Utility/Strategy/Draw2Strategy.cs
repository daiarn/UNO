using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw2Strategy : ICardStrategy
	{
		public void Action()
		{
			var game = Game.GetInstance();
			var targetPlayer = game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)];

			targetPlayer.hand.Add(game.FromDrawPile());
			targetPlayer.hand.Add(game.FromDrawPile());

			game.SkipNextPlayerTurn();
		}
	}
}