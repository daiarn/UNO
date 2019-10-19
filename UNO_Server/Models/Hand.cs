using System.Collections.Generic;

namespace UNO_Server.Models
{
	public class Hand
	{
		public List<Card> cards { get; set; }

		public Hand()
		{
			cards = new List<Card>();
		}

		public void Add(Card card)
		{
			cards.Add(card);
		}

		public int Count()
		{
			return cards.Count;
		}

		public void Remove(Card card)
		{
			cards.Remove(card);
		}

		public bool Contains(Card card)
		{
			return cards.Contains(card);
			/*
			foreach (var item in cards)
			{
				if (item.Equals(card))
				{
					return true;
				}
			}
			return false;//*/
		}

		public Card GetCard(int i)
		{
			return cards[i];
		}
	}
}
