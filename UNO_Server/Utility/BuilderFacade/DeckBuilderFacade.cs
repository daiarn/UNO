using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderFacade
	{
		protected DeckInfo info;

		public DeckBuilderFacade()
		{
			info = new DeckInfo();
		}
		private void AddAllColors(Deck deck, CardType type)
		{
			deck.AddToBottom(new Card(CardColor.Red, type));
			deck.AddToBottom(new Card(CardColor.Yellow, type));
			deck.AddToBottom(new Card(CardColor.Green, type));
			deck.AddToBottom(new Card(CardColor.Blue, type));
		}

		public Deck Build()
		{
			var deck = new Deck();

			// number cards

			for (int j = 0; j < info.numberCards[0]; j++)
				AddAllColors(deck, CardType.Zero);
			for (int j = 0; j < info.numberCards[1]; j++)
				AddAllColors(deck, CardType.One);
			for (int j = 0; j < info.numberCards[2]; j++)
				AddAllColors(deck, CardType.Two);
			for (int j = 0; j < info.numberCards[3]; j++)
				AddAllColors(deck, CardType.Three);
			for (int j = 0; j < info.numberCards[4]; j++)
				AddAllColors(deck, CardType.Four);
			for (int j = 0; j < info.numberCards[5]; j++)
				AddAllColors(deck, CardType.Five);
			for (int j = 0; j < info.numberCards[6]; j++)
				AddAllColors(deck, CardType.Six);
			for (int j = 0; j < info.numberCards[7]; j++)
				AddAllColors(deck, CardType.Seven);
			for (int j = 0; j < info.numberCards[8]; j++)
				AddAllColors(deck, CardType.Eight);
			for (int j = 0; j < info.numberCards[9]; j++)
				AddAllColors(deck, CardType.Nine);

			// action cards

			for (int j = 0; j < info.skipCards; j++)
				AddAllColors(deck, CardType.Skip);

			for (int j = 0; j < info.reverseCards; j++)
				AddAllColors(deck, CardType.Reverse);

			for (int j = 0; j < info.draw2Cards; j++)
				AddAllColors(deck, CardType.Draw2);

			// wild action cards

			for (int j = 0; j < info.wildCards; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Wild));

			for (int j = 0; j < info.draw4Cards; j++)
				deck.AddToBottom(new Card(CardColor.Black, CardType.Draw4));

			return deck;
		}

		public DeckBuilderNumberFacade number => new DeckBuilderNumberFacade(info);
		public DeckBuilderActionFacade action => new DeckBuilderActionFacade(info);
		public DeckBuilderWildFacade wild => new DeckBuilderWildFacade(info);
	}
}
