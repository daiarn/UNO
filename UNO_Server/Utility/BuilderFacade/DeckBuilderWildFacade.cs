namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckBuilderWildFacade : DeckBuilderFacade
	{
		public DeckBuilderWildFacade(DeckInfo info)
		{
			this.info = info;
		}

		public DeckBuilderWildFacade SetWildCards(int num)
		{
			info.wildCards = num;
			return this;
		}

		public DeckBuilderWildFacade SetDraw4Cards(int num)
		{
			info.draw4Cards = num;
			return this;
		}

		public DeckBuilderWildFacade SetBlackCards(int num)
		{
			info.wildCards = num;
			info.draw4Cards = num;
			return this;
		}
	}
}
