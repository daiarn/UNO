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

		public DeckBuilderNumberFacade AddIndividualNumberCards(int card, int num)
		{
			if (card < 0 || card > 9) return this;
			AddAllColors(deck, Card.numberCardTypes[num]);
			return this;
		}

	}
}
