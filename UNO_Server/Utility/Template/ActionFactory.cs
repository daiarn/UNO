using UNO_Server.Models;

namespace UNO_Server.Utility.Template
{
	public class ActionFactory
	{
		public BaseTemplate Create(CardType type)
		{
			switch (type)
			{
				case CardType.Skip:
					return new SkipTemplate();
				case CardType.Reverse:
					return new ReverseTemplate();
				case CardType.Draw2:
					return new Draw2Template();
				case CardType.Draw4:
					return new Draw4Template();
				default:
					return new BaseTemplate();
			}
		}
	}
}
