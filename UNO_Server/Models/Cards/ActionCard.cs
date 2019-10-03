using System;

namespace UNO.Models
{
	public enum ActionType
	{
		Skip, Reverse, Draw2, Wild, Draw4
	}
    abstract class ActionCard : Card
    {
        protected ActionType type { get; set; }

        public ActionCard(Color color, ActionType action) : base(color)
        {
            this.type = action;
        }

		public abstract void action();
    }
}
