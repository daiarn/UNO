using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	class Draw2Strategy : ICardStrategy
    {
        public void Action()
        {
			var game = Game.GetInstance();

			var victimPlayer = game.GetNextPlayerAfter(game.activePlayerIndex);
			victimPlayer.hand.Add(game.FromDrawPile());
			victimPlayer.hand.Add(game.FromDrawPile());

			game.NextPlayerTurn();
		}
    }
}