using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO_Server.Utility
{
	public abstract class Observer
	{
		public int Counter { get; set; }
		public abstract void Notify(Card card);
	}
}
