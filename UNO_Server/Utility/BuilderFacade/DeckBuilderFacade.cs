using UNO_Server.Models;
using UNO_Server.Utility.BuilderFacade;

namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderFacade
	{
		protected Deck deck;

		public DeckBuilderFacade()
		{
			deck = new Deck();
		}

		protected void addAllColors(Deck deck, CardType type)
		{
			deck.AddToBottom(new Card(CardColor.Red, type));
			deck.AddToBottom(new Card(CardColor.Yellow, type));
			//deck.AddToBottom(new Card(CardColor.Green, type));
			//deck.AddToBottom(new Card(CardColor.Blue, type));
		}

		public Deck build()
		{
			return deck;
		}

		public DeckBuilderNumberFacade number => new DeckBuilderNumberFacade(deck);
		public DeckBuilderActionFacade action => new DeckBuilderActionFacade(deck);
		public DeckBuilderWildFacade wild => new DeckBuilderWildFacade(deck);
	}
}
