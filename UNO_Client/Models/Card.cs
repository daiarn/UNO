using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Properties;

namespace UNO_Client.Models
{
    class Card
    {
        public string Color { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }

        public Bitmap GetImage()
        {
            return (Bitmap) Resources.ResourceManager.GetObject(this.Color + this.Value);
        }
    }
}
