using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw4Strategy : ICardStrategy
    {
        public void Action()
		{
			var game = Game.GetInstance();
			for (int i = 0; i < 4; i++)
            {
				game.nextPlayer.hand.Add(game.FromDrawPile());
            }
			game.NextPlayerTurn();
        }
    }
}