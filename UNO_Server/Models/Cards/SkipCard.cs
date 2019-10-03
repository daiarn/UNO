using System;

namespace UNO.Models
{
	class SkipCard : ActionCard
	{
		public SkipCard(Color color) : base(color, ActionType.Skip) { }

		public override void action()
		{
			throw new NotImplementedException();
		}
	}
}
