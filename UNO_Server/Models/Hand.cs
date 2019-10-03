using System.Collections.Generic;

namespace UNO.Models
{
	class Hand
	{
		private List<Card> cards { get; set; }

		public void add(Card card)
		{
			cards.Add(card);
		}
	}
}
