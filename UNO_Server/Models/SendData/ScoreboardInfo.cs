using System.Collections.Generic;
using System.Linq;

namespace UNO_Server.Models.SendData
{
	public class ScoreboardInfo
	{
		public int index;
		public int score;
		public int turn;

		public ScoreboardInfo(int i, IEnumerable<Card> hand, int t)
		{
			index = i;
			score = hand.Aggregate(0, (sum, next) => sum + next.GetScore());
			turn = t;
		}
		public ScoreboardInfo(int i)
		{
			index = i;
			score = 0;
			turn = -1;
		}
	}
}
