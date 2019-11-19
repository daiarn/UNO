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
        float x;
        float y;
        Color color;
        Graphics graphics;

        public Star(float x, float y, Color color, Graphics graphics)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.graphics = graphics;
        }

        public void Paint()
        {
            SolidBrush brush = new SolidBrush(color);
            Font customFont = new System.Drawing.Font("Helvetica", 80, FontStyle.Bold);
            graphics.DrawString("*", customFont, brush, x, y);
        }
    }
}
