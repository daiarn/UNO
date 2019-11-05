using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderWildFacade : DeckBuilderFacade
	{
		public DeckBuilderWildFacade(Deck deck)
		{
			this.deck = deck;
		}
		public DeckBuilderWildFacade AddWildCards(int num)
		{
			for (int j = 0; j < num; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Wild));
			return this;
		}

		public DeckBuilderWildFacade AddDraw4Cards(int num)
		{
			for (int j = 0; j < num; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Draw4));
			return this;
		}

		public DeckBuilderWildFacade AddBlackCards(int num)
		{
			for (int j = 0; j < num; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Wild));

			for (int j = 0; j < num; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Draw4));
			return this;
		}

	}
}
