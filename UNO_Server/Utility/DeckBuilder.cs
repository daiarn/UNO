using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO.Models;

namespace UNO_Server.Utility
{
	public class DeckBuilder
	{
		private int[] numberCards; // all numbers 0-9

		// action cards
		private int skipCards;
		private int reverseCards;
		private int draw2Cards;

		// wild action cards
		private int wildCards;
		private int draw4Cards;


		public DeckBuilder()
		{
			numberCards = new int[10];

			setNumberCards(2);
			setIndividualNumberCards(0, 1);
			setActionCards(2);
			setBlackCards(4);
		}

		// aggregate cards

		public void setNumberCards(int num)
		{
			for (int i = 0; i < 10; i++)
			{
				numberCards[i] = num;
			}
		}

		public void setActionCards(int num)
		{
			skipCards = num;
			reverseCards = num;
			draw2Cards = num;
			wildCards = num;
			draw4Cards = num;
		}

		public void setBlackCards(int num)
		{
			wildCards = num;
			draw4Cards = num;
		}

		// individual cards

		public void setIndividualNumberCards(int card, int num)
		{
			if (card < 0 || card > 9) return;
			numberCards[card] = num;
		}

		public void setSkipCards(int num)
		{
			skipCards = num;
		}

		public void setReverseCards(int num)
		{
			reverseCards = num;
		}

		public void setDraw2Cards(int num)
		{
			draw2Cards = num;
		}

		public void setWildCards(int num)
		{
			wildCards = num;
		}

		public void setDraw4Cards(int num)
		{
			draw4Cards = num;
		}

		private void addAllColors(Deck deck, CardType type)
		{
			deck.AddToBottom(new Card(CardColor.Red, type));
			deck.AddToBottom(new Card(CardColor.Yellow, type));
			deck.AddToBottom(new Card(CardColor.Green, type));
			deck.AddToBottom(new Card(CardColor.Blue, type));
		}

		public Deck build()
		{
			Deck deck = new Deck();

			// number cards

			for (int j = 0; j < numberCards[0]; j++)
				addAllColors(deck, CardType.Zero);
			for (int j = 0; j < numberCards[1]; j++)
				addAllColors(deck, CardType.One);
			for (int j = 0; j < numberCards[2]; j++)
				addAllColors(deck, CardType.Two);
			for (int j = 0; j < numberCards[3]; j++)
				addAllColors(deck, CardType.Three);
			for (int j = 0; j < numberCards[4]; j++)
				addAllColors(deck, CardType.Four);
			for (int j = 0; j < numberCards[5]; j++)
				addAllColors(deck, CardType.Five);
			for (int j = 0; j < numberCards[6]; j++)
				addAllColors(deck, CardType.Six);
			for (int j = 0; j < numberCards[7]; j++)
				addAllColors(deck, CardType.Seven);
			for (int j = 0; j < numberCards[8]; j++)
				addAllColors(deck, CardType.Eight);
			for (int j = 0; j < numberCards[9]; j++)
				addAllColors(deck, CardType.Nine);

			// action cards

			for (int j = 0; j < skipCards; j++)
				addAllColors(deck, CardType.Skip);

			for (int j = 0; j < reverseCards; j++)
				addAllColors(deck, CardType.Reverse);

			for (int j = 0; j < draw2Cards; j++)
				addAllColors(deck, CardType.Draw2);

			// wild action cards

			for (int j = 0; j < wildCards; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Wild));

			for (int j = 0; j < draw4Cards; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Draw4));

			return deck;
		}
	}
}
