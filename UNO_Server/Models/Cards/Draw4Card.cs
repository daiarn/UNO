using System;

namespace UNO.Models
{
	class Draw4Card : WildCard // maybe change to regular ActionCard?
	{
		public Draw4Card() : base()
		{
			type = ActionType.Draw4;
		}
		public Draw4Card(Color color) : base(color)
		{
			type = ActionType.Draw4;
		}

		public override void action()
		{
			throw new NotImplementedException();
		}
	}
}
