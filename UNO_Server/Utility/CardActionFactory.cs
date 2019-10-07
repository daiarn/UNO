using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;
using UNO_Server.Models;

namespace UNO_Server.Utility
{
	public class CardActionFactory : Factory
	{
		public CardActionFactory()
		{
		}

		public override ICardStrategy CreateAction(CardType type)
		{
			switch (type)
			{
				case CardType.Skip:
					return new SkipStrategy();
				case CardType.Reverse:
					return new ReverseStrategy();
				case CardType.Draw2:
					return new Draw2Strategy();
				case CardType.Wild:
					return new WildStrategy();
				case CardType.Draw4:
					return new Draw4Strategy();
				default:
					return null;
			}
		}
	}
}
