using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderActionFacade : DeckBuilderFacade
	{
		public DeckBuilderActionFacade(Deck deck)
		{
			this.deck = deck;
		}

		public DeckBuilderActionFacade AddActionCards(int num)
		{
			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Skip);

			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Reverse);

			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Draw2);

			return this;
		}

		public DeckBuilderActionFacade AddSkipCards(int num)
		{
			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Skip);
			return this;
		}

		public DeckBuilderActionFacade AddReverseCards(int num)
		{
			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Reverse);
			return this;
		}

		public DeckBuilderActionFacade AddDraw2Cards(int num)
		{
			for (int j = 0; j < num; j++)
				AddAllColors(deck, CardType.Draw2);

			return this;
		}

	}
}
