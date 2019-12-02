using System.Collections.Generic;
using System.Linq;

namespace UNO_Server.Models.SendData
{
	public class GameSpectatorState
	{
		public int zeroCounter;
		public int wildCounter;
        public int cardCounter;
        public int skipCounter;
        public int moveCounter;

		public GamePhase phase;
		public int discardPileCount;
		public int drawPileCount;
		public Card activeCard;

		public int activePlayer;
        public GamePhase gamePhase;
		public List<PlayerInfo> players;
		public ScoreboardInfo[] scoreboard;

		public GameSpectatorState(Game game)
		{
            zeroCounter = game.gameWatcher.observers[0].Counter;
            wildCounter = game.gameWatcher.observers[1].Counter;
            cardCounter = game.cardCount;
            skipCounter = game.skipCount;
            moveCounter = game.moveCount;

            phase = game.phase;
			discardPileCount = game.discardPile.Count();
			drawPileCount = game.drawPile.Count();
			activeCard = game.discardPile.PeekBottomCard();

			activePlayer = game.activePlayerIndex;
            gamePhase = game.phase;
            players = game.players.Where(p => p != null).Select(p => new PlayerInfo(p)).ToList();
			scoreboard = game.scoreboard;
		}
	}
}
