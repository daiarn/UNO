using System;

namespace UNO.Models
{
	class ReverseCard : ActionCard
	{
		public ReverseCard(Color color) : base(color, ActionType.Reverse) { }

		public override void action()
		{
			throw new NotImplementedException();
		}
	}
}
