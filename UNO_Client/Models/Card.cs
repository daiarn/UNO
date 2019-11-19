namespace UNO_Client.Models
{
	class Card
	{
		int color;
		int type;

		public Card(int color, int type)
		{
			this.color = color;
			this.type = type;
		}

		public int Color { get => color; set => color = value; }
		public int Type { get => type; set => type = value; }
	}
}
