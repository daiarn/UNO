using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Utility
{
    public abstract class RectDecorator : Rect
    {
        protected readonly Rect rectangleToBeDecorated;
        public RectDecorator(Rect rectangleToBeDecorated)
        {
            this.rectangleToBeDecorated = rectangleToBeDecorated;
            this.graphics = rectangleToBeDecorated.graphics;
            this.rectangle = rectangleToBeDecorated.rectangle;
        }

        public override void Draw()
        {
            rectangleToBeDecorated.Draw();
        }
    }
}
