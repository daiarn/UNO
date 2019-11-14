using UNO_Server.Models;

namespace UNO_Server.Utility.Command
{
	public class DrawCard : ICommand
	{
		int playerNumber;

		public void Execute()
		{
			playerNumber = Game.GetInstance().activePlayerIndex;
			Game.GetInstance().PlayerDrawsCard();
		}

		public void Undo()
		{
			if (playerNumber != -1)
			{
				var game = Game.GetInstance();

				int index = game.players[playerNumber].hand.Count - 1;
				Card card = game.players[playerNumber].hand[index];
				game.players[playerNumber].hand.Remove(card);
				game.drawPile.AddtoTop(card);
			}
			playerNumber = -1;
		}
	}
}
