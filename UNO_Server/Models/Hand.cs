﻿using System;
using System.Collections.Generic;

namespace UNO.Models
{
	public class Hand
	{
		private List<Card> cards { get; set; }

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
		}

		public Card getCard(int i)
		{
			return cards[i];
		}
	}
}