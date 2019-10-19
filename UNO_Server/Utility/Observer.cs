using UNO_Server.Models;

namespace UNO_Server.Utility
{
	public abstract class Observer
	{
		public int Counter { get; set; }
		public abstract void Notify(Card card);
	}
}
