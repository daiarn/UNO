using System.Collections.Generic;
using System.Linq;

namespace UNO_Server.Models.SendData
{
	public class GameSpectatorState
	{
		public int zeroCounter;
		public int wildCounter;

		public GamePhase phase;
		public int discardPileCount;
		public int drawPileCount;
		public Card activeCard;

		public int activePlayer;
        public GamePhase gamePhase;
		public List<PlayerInfo> players;
		public WinnerInfo[] winners;

		public GameSpectatorState(Game game)
		{
			zeroCounter = game.observers[0].Counter;
			wildCounter = game.observers[1].Counter;

			phase = game.phase;
			discardPileCount = game.discardPile.GetCount();
			drawPileCount = game.drawPile.GetCount();
			activeCard = game.discardPile.PeekBottomCard();

			activePlayer = game.activePlayerIndex;
            gamePhase = game.phase;
            players = game.players.Where(p => p != null).Select(p => new PlayerInfo(p)).ToList();
			winners = game.winners;
		}
	}
}
