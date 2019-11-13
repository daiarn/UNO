﻿using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	public class CardActionFactory : StrategyFactory
	{
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
				case CardType.Draw4:
					return new Draw4Strategy();
				default:
					return null;
			}
		}
	}
}