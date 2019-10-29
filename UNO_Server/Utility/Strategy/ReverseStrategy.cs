using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class ReverseStrategy : ICardStrategy
    {
        public void Action()
		{
			var game = Game.GetInstance();
			if (game.GetActivePlayerCount() <= 2)
				game.NextPlayerSkipsTurn();
			else
			{
				game.ReverseFlow();
				game.nextPlayerIndex = game.GetNextPlayerIndexAfter(game.activePlayerIndex);
			}
		}
    }
}