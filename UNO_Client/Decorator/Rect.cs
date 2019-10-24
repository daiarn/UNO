using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Decorator
{
    public abstract class Rect
    {
        public RectangleF rectangle;
        public Graphics graphics;
        public abstract void Draw();
    }
}
