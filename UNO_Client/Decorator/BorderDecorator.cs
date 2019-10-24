using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UNO_Client.Decorator
{
    public class BorderDecorator : RectDecorator
    {
        public BorderDecorator(Rect rectangleToBeDecorated) : base(rectangleToBeDecorated)
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
            rectangleToBeDecorated.graphics.DrawRectangle(blackPen, roundedRectangle);
        }
    }
}
