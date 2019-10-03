using System;

namespace UNO.Models
{
    class WildCard : ActionCard
	{
		public WildCard() : base(Color.Black, ActionType.Wild) { }
		public WildCard(Color color) : base(color, ActionType.Wild) { }

		public override void action()
		{
			throw new NotImplementedException();
		}
	}
}
