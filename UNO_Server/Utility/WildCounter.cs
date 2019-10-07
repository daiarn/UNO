using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO_Server.Utility
{
	public class WildCounter : Observer
	{
		public override void Notify(Card card)
		{
			if (card.type == CardType.Wild || card.type == CardType.Draw4) Counter++;
		}
	}
}
