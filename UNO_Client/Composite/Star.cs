using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Composite
{
    class Star : Graphic
    {
        RectangleF rectangle;
        Graphics graphics;

        public Star(RectangleF rectangle, Graphics graphics)
        {
            this.rectangle = rectangle;
            this.graphics = graphics;
        }

        public void Paint()
        {
            
        }
    }
}
