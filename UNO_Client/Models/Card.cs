using System.Drawing;

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


		private string buildImageString(int color, int type)
		{
			string img = "";
			switch (color)
			{
				case 1:
					img = "r";
					break;
				case 2:
					img = "y";
					break;
				case 3:
					img = "g";
					break;
				case 4:
					img = "b";
					break;
			}

			switch (type)
			{
				case 0:
					img += "0";
					break;
				case 1:
					img += "1";
					break;
				case 2:
					img += "2";
					break;
				case 3:
					img += "3";
					break;
				case 4:
					img += "4";
					break;
				case 5:
					img += "5";
					break;
				case 6:
					img += "6";
					break;
				case 7:
					img += "7";
					break;
				case 8:
					img += "8";
					break;
				case 9:
					img += "9";
					break;

				case 10: // skip
					img += "s";
					break;
				case 11: // reverse
					img += "r";
					break;
				case 12: // draw2
					img += "p";
					break;

				case 13: // wild
					img += "c";
					break;
				case 14: // draw4
					img += "p4";
					break;
			}

			return img;
		}

		public Bitmap GetImage()
		{
			//return (Bitmap) Resources.ResourceManager.GetObject(this.Color + this.Value);
			return new Bitmap("..//..//CardImages//" + buildImageString(color, type) + ".png");
		}
	}
}
