using System;

namespace UNO.Models
{
	public enum Color
	{
		Red, Yellow, Green, Blue, Black
	}

    public abstract class Card
    {
        public Color color { get; set; }
        //private String icon { get; set; }

        public Card(Color color)
        {
            this.color = color;
        }
    }
}
