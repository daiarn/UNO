using System.Collections.Generic;
using UNO_Server.Models;

namespace UNO_Server.Utility.Memento
{
	public class GameMemento
	{
		private GamePhase phase;

		private bool flowClockWise { get; set; }
		private Deck drawPile { get; set; }
		private Deck discardPile { get; set; }

		public List<Card> hand1;
		public List<Card> hand2;
		public int numPlayers;

		public int player1Index;
		public int player2Index;

		public GameMemento(Game game)
		{
			phase = game.phase;
			flowClockWise = game.flowClockWise;
			drawPile = game.drawPile.MakeDeepCopy();
			discardPile = game.discardPile.MakeDeepCopy();
			numPlayers = game.numPlayers;
			player1Index = game.activePlayerIndex;
			player2Index = game.GetNextPlayerIndexAfter(game.activePlayerIndex);
			hand1 = game.players[player1Index].hand.ConvertAll(card => new Card(card));
			hand2 = game.players[player2Index].hand.ConvertAll(card => new Card(card));

		}

		public void Restore(Game game)
		{
			game.phase = phase;
			game.flowClockWise = flowClockWise;
			game.drawPile = drawPile;
			game.discardPile = discardPile;
			game.activePlayerIndex = player1Index;
			game.players[player1Index].hand = hand1;
			game.players[player2Index].hand = hand2;
			game.numPlayers = numPlayers;
		}
	}
}
