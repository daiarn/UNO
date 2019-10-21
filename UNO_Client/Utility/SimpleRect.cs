using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Utility
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
