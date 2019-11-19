using System.Drawing;

namespace UNO_Client.Decorator
{
	public abstract class Rect
	{
		public RectangleF rectangle;
		public Graphics graphics;
		public abstract void Draw();
	}
}
