namespace UNO_Server.Utility.BuilderFacade
{
	public class DeckInfo
	{
		// all numbers 0-9
		public int[] numberCards;

		// action cards
		public int skipCards;
		public int reverseCards;
		public int draw2Cards;

		// wild action cards
		public int wildCards;
		public int draw4Cards;
		public DeckInfo()
		{
			numberCards = new int[10];
		}
	}
}
