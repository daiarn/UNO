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

		public void Shuffle()
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

		public void AddToBottom(Card card)
		{
			cards.Add(card);
		}

		public Card DrawTopCard() // removes card from deck and returns said card
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

		public Card DrawBottomCard() // removes card from deck and returns said card
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

		public Card PeekBottomCard()
		{
			return cards.Last();
		}

		public int GetCount()
		{
			return cards.Count;
		}
	}
}