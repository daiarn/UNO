using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderNumberFacade : DeckBuilderFacade
	{
		public DeckBuilderNumberFacade(Deck deck)
		{
			this.deck = deck;
		}

		public DeckBuilderNumberFacade AddNonZeroNumberCards(int num)
		{
			for (int i = 1; i < 10; i++)
				for (int j = 0; j < num; j++)
					AddAllColors(deck, Card.numberCardTypes[i]);

			return this;
		}

		public DeckBuilderNumberFacade AddIndividualNumberCards(int cardNumber, int amount)
		{
			if (cardNumber < 0 || cardNumber > 9) return this;
			AddAllColors(deck, Card.numberCardTypes[amount]);
			return this;
		}

	}
}
