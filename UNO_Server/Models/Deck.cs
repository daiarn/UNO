using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO.Models
{
	public class Deck
	{
		private List<Card> cards { get; set; }

		public Deck()
		{
			cards = new List<Card>();
		}

		public void shuffle()
		{
			var arr = cards.ToArray();
			var rand = new Random();

			for (int i = arr.Length - 1; i > 0; i--)
			{
				int j = rand.Next(i + 1);

				var temp = arr[j];
				arr[i] = arr[j];
				arr[j] = temp;
			}

			cards = arr.ToList();
		}

		public void addToBottom(Card card)
		{
			cards.Add(card);
		}

		public Card drawTopCard() // removes card from deck and returns said card
		{
			if (cards.Count > 0)
			{
				var card = cards.First();
				cards.RemoveAt(0);
				return card;
			}
			else
				return null;
		}

		public Card drawBottomCard() // removes card from deck and returns said card
		{
			if (cards.Count > 0)
			{
				var card = cards.Last();
				cards.RemoveAt(cards.Count - 1);
				return card;
			}
			else
				return null;
		}

		public int getCount()
		{
			return cards.Count;
		}
	}
}