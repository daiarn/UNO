using System.Collections.Generic;
using System.Linq;

namespace UNO_Server.Models.SendData
{
	public class WinnerInfo
	{
		public int index;
		public int score;
		public int turn;

		public WinnerInfo(int i, IEnumerable<Card> hand, int t)
		{
			index = i;
			score = hand.Aggregate(0, (sum, next) => sum + next.GetScore());
			turn = t;
		}
		public WinnerInfo(int i)
		{
			index = i;
			score = 0;
			turn = -1;
		}
	}
}
