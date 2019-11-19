using System.Drawing;

namespace UNO_Client.Decorator
{
	public class DiagonalDecorator : RectDecorator
	{
		public DiagonalDecorator(Rect rectangleToBeDecorated) : base(rectangleToBeDecorated)
		{
			this.rectangle = rectangleToBeDecorated.rectangle;
			this.graphics = rectangleToBeDecorated.graphics;
		}

		public override void Draw()
		{
			base.Draw();
			Pen blackPen = new Pen(Color.Black, 2);
			blackPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
			Rectangle roundedRectangle = Rectangle.Round(rectangleToBeDecorated.rectangle);
			//rectangleToBeDecorated.graphics.DrawRectangle(blackPen, roundedRectangle);
			rectangleToBeDecorated.graphics.DrawLine(blackPen, roundedRectangle.Left, roundedRectangle.Top, roundedRectangle.Right, roundedRectangle.Bottom);
			rectangleToBeDecorated.graphics.DrawLine(blackPen, roundedRectangle.Right, roundedRectangle.Top, roundedRectangle.Left, roundedRectangle.Bottom);
		}
	}
}
