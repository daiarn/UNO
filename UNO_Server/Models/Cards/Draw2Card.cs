using System;

namespace UNO.Models
{
    class Draw2Card : ActionCard
    {
		public Draw2Card(Color color) : base(color, ActionType.Draw2) { }

		public override void action()
		{
			throw new NotImplementedException();
		}
	}
}
