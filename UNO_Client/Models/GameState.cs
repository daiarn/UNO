using System.Collections.Generic;

namespace UNO_Client.Models
{
	public class GameState
    {
		public int ZeroCounter { get; set; }
		public int WildCounter { get; set; }
        public int CardCounter { get; set; }
        public int SkipCounter { get; set; }
        public int MoveCounter { get; set; }
        public int DiscardPile { get; set; }
		public int DrawPile { get; set; }
		public int ActivePlayer { get; set; }
		public Card ActiveCard { get; set; }
        public List<Player> Players { get; set; }
		public List<Card> Hand { get; set; }
		public int Index { get; set; }
		public GamePhase GamePhase { get; set; }
		public int WinnerIndex { get; set; }
	}
}
