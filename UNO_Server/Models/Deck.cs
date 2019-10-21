using System;
using System.Collections.Generic;
using System.Linq;

namespace UNO_Server.Models
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

			for (int i = 0; i < arr.Length - 2; i++)
			{
				int j = i + rand.Next(arr.Length - i);

				var temp = arr[i];
				arr[i] = arr[j];
				arr[j] = temp;
			}

			cards = arr.ToList();
		}

		public void AddToBottom(Card card)
		{
			cards.Add(card);
		}

        public void AddtoTop(Card card)
        {
            cards.Insert(0, card);
        }

		public Card DrawTopCard()
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

		public Card DrawBottomCard()
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
			return cards.LastOrDefault();
		}

		public int GetCount()
		{
			return cards.Count;
		}
	}
}