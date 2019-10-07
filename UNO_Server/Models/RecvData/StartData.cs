using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Models
{
	public class StartData
	{
		public Guid id { get; set; }
		public bool finiteDeck { get; set; }
		public bool onlyNumbers { get; set; }
	}
}
