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
        string color;
        string value;
        string image;

        public string Color { get => color; set => color = value; }
        public string Value { get => value; set => this.value = value; }
        public string Image { get => image; set => image = value; }

        public Bitmap GetImage()
        {
            return (Bitmap) Resources.ResourceManager.GetObject(this.Color + this.Value);
        }
    }
}
