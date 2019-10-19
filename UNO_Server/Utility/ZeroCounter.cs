using UNO_Server.Models;

namespace UNO_Server.Utility
{
	public class ZeroCounter : Observer
	{
		public override void Notify(Card card)
		{
			if (card.type == CardType.Zero) Counter++;
		}
	}
}
