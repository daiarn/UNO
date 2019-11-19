using System.Drawing;

namespace UNO_Client.Decorator
{
	public class SimpleRect : Rect
	{
		public SimpleRect(RectangleF rectangle, Graphics graphics)
		{
			this.rectangle = rectangle;
			this.graphics = graphics;
		}
		public override void Draw()
		{
			//nothing
		}
	}
}
