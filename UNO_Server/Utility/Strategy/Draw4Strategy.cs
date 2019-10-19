using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw4Strategy : ICardStrategy
    {
        public void Action()
		{
			var game = Game.GetInstance();
			var targetPlayer = game.GetNextPlayer();

			for (int i = 0; i < 4; i++)
				targetPlayer.hand.Add(game.FromDrawPile());

			game.NextPlayerSkipsTurn();
		}
    }
}