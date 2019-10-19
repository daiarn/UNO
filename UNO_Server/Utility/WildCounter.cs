using UNO_Server.Models;

namespace UNO_Server.Utility
{
	public class WildCounter : Observer
	{
		public override void Notify(Card card)
		{
			if (card.type == CardType.Wild || card.type == CardType.Draw4) Counter++;
		}
	}
}
