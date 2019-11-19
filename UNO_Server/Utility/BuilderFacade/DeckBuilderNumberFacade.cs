namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderNumberFacade : DeckBuilderFacade
	{
		public DeckBuilderNumberFacade(DeckInfo info)
		{
			this.info = info;
		}

		public DeckBuilderNumberFacade SetAllNumberCards(int num)
		{
			for (int i = 0; i < 10; i++)
				info.numberCards[i] = num;

			return this;
		}

		public DeckBuilderNumberFacade SetIndividualNumberCards(int cardNumber, int amount)
		{
			if (cardNumber < 0 || cardNumber > 9) return this;
			info.numberCards[cardNumber] = amount;
			return this;
		}
	}
}
