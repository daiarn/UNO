namespace UNO_Server.Models.SendData
{
	public class GameState
	{
		public int zeroCounter;
		public int wildCounter;

		public int discardPileCount;
		public int drawPileCount;
		public Card activeCard;

		public int activePlayer;
		//public var players;

		//public Card[] hand;

		public GameState(Game game)
		{
			zeroCounter = game.observers[0].Counter;
			wildCounter = game.observers[1].Counter;

			discardPileCount = game.discardPile.GetCount();
			drawPileCount = game.drawPile.GetCount();
			activeCard = game.discardPile.PeekBottomCard();

			activePlayer = game.activePlayerIndex;
			//players = ...;
		}
	}
}
