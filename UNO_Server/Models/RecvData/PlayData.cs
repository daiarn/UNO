using System;

namespace UNO_Server.Models.RecvData
{
	public class PlayData
	{
		public Guid id { get; set; }
		public CardColor color { get; set; }
		public CardType type { get; set; }
	}
}
