using System.Linq;

namespace UNO_Server.Models.SendData
{
	public class WinnerInfo
	{
		public string name;
		public int score;
		public int turn;

		public WinnerInfo(Player player, int t)
		{
			name = player.name;
			score = player.hand.Aggregate(0, (sum, next) => sum + next.GetScore());
			turn = t;
		}
		public WinnerInfo(Player player)
		{
			name = player.name;
			score = 0;
			turn = -1;
		}
	}
}
