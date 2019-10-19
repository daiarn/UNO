using System;

namespace UNO_Server.Models
{
	public class PlayData
	{
		public Guid id { get; set; }
		public CardColor color { get; set; }
		public CardType type { get; set; }
	}
}
