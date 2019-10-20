﻿using System;
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
        string type;
        string image;

        public Card(string color, string value, string image)
        {
            this.color = color;
            this.type = value;
            this.image = image;
        }

        public string Color { get => color; set => color = value; }
        public string Type { get => type; set => this.type = value; }
        public string Image { get => image; set => image = value; }

        public Bitmap GetImage()
        {
            //return (Bitmap) Resources.ResourceManager.GetObject(this.Color + this.Value);
            return new Bitmap("..//..//CardImages//" + Image + ".png");
        }
    }
}
