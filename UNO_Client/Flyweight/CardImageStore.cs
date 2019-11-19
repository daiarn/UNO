using System.Collections.Concurrent;
using System.Drawing;
using UNO_Client.Models;

namespace UNO_Client.Flyweight
{
	class CardImageStore
	{
		private static readonly ConcurrentDictionary<string, Bitmap> _cache = new ConcurrentDictionary<string, Bitmap>();
		public static Bitmap GetImage(Card card)
		{
			string path = BuildImageString(card);
			return _cache.GetOrAdd(path, filename => new Bitmap("..//..//CardImages//" + filename + ".png"));
		}

		public static string BuildImageString(Card card)
		{
			int color = card.Color;
			int type = card.Type;

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
					img += "d";
					break;

				case 13: // wild
					img += "w";
					break;
				case 14: // draw4
					img += "d4";
					break;
			}

			return img;
		}
	}
}
