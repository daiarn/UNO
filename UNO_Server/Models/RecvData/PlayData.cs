using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO_Server.Models
{
	public class PlayData
	{
		public Guid id { get; set; }
		public CardColor color { get; set; }
		public CardType type { get; set; }
	}
}
