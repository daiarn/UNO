using UNO.Models;

namespace UNO.Patterns
{
	class DeckBuilder
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

		public Deck build()
		{
			Deck deck = new Deck();

			// number cards

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < numberCards[i]; j++)
				{
					deck.add(new NumberCard("red", i));
					deck.add(new NumberCard("yellow", i));
					deck.add(new NumberCard("green", i));
					deck.add(new NumberCard("blue", i));
				}
			}

			// action cards

			for (int j = 0; j < skipCards; j++)
			{
				deck.add(new SkipCard()); // add all colors
			}

			for (int j = 0; j < reverseCards; j++)
			{
				deck.add(new ReverseCard()); // add all colors
			}

			for (int j = 0; j < draw2Cards; j++)
			{
				deck.add(new Draw2Card()); // add all colors
			}

			// wild action cards

			for (int j = 0; j < wildCards; j++)
				deck.add(new WildCard());

			for (int j = 0; j < draw4Cards; j++)
				deck.add(new Draw4Card());

			return deck;
		}
	}
}