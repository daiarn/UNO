using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO_Server.Utility
{
	public class ZeroCounter : Observer
	{
		public override void Notify(Card card)
		{
			if (card.type == CardType.Zero) Counter++;
		}
	}
}
