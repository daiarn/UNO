using System;

namespace UNO_Server.Models.RecvData
{
	public class StartData
	{
		public Guid id { get; set; }
		public bool finiteDeck { get; set; }
		public bool onlyNumbers { get; set; }
		public bool slowGame { get; set; }
	}
}
