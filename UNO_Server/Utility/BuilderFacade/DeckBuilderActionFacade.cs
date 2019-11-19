namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderActionFacade : DeckBuilderFacade
	{
		public DeckBuilderActionFacade(DeckInfo info)
		{
			this.info = info;
		}

		public DeckBuilderActionFacade SetActionCards(int num)
		{
			info.skipCards = num;
			info.reverseCards = num;
			info.draw2Cards = num;
			return this;
		}

		public DeckBuilderActionFacade SetSkipCards(int num)
		{
			info.skipCards = num;
			return this;
		}

		public DeckBuilderActionFacade SetReverseCards(int num)
		{
			info.reverseCards = num;
			return this;
		}

		public DeckBuilderActionFacade SetDraw2Cards(int num)
		{
			info.draw2Cards = num;
			return this;
		}
	}
}
