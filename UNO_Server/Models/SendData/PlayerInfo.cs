namespace UNO_Server.Models.SendData
{
	public class PlayerInfo
	{
		public string name;
		public int cardCount;
		public bool isPlaying;

		public PlayerInfo(Player player)
		{
			name = player.name;
			cardCount = player.hand.GetCount();
			isPlaying = player.isPlaying;
		}
	}
}
