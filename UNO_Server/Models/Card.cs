using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UNO_Server.Models
{
	public enum CardColor
	{
		Black, Red, Yellow, Green, Blue
	}

	public enum CardType
	{
		Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Skip, Reverse, Draw2, Wild, Draw4
	}

	[DebuggerDisplay("{color}   {type}")]
	public class Card : IEquatable<Card>
    {
		public CardColor color;
		public CardType type;
        public string image;

		public static readonly List<CardType> numberCardTypes = new List<CardType> {
				CardType.Zero, CardType.One, CardType.Two, CardType.Three, CardType.Four, CardType.Five, CardType.Six, CardType.Seven, CardType.Eight, CardType.Nine
			};

		public Card(CardColor color, CardType type)
		{
			this.color = color;
			this.type = type;
			this.image = buildImageString(color, type);
		}
		public Card(Card other)
		{
			this.color = other.color;
			this.type = other.type;
			this.image = other.image;
		}

		public bool Equals(Card other)
		{
			if (type == CardType.Wild && other.type == CardType.Wild) return true;
			if (type == CardType.Draw4 && other.type == CardType.Draw4) return true;
			return color == other.color && type == other.type;
		}

		public int GetScore()
		{
			switch (type)
			{
				case CardType.Zero:
					return 0;
				case CardType.One:
					return 1;
				case CardType.Two:
					return 2;
				case CardType.Three:
					return 3;
				case CardType.Four:
					return 4;
				case CardType.Five:
					return 5;
				case CardType.Six:
					return 6;
				case CardType.Seven:
					return 7;
				case CardType.Eight:
					return 8;
				case CardType.Nine:
					return 9;
				case CardType.Skip:
				case CardType.Reverse:
				case CardType.Draw2:
					return 20;
				case CardType.Wild:
				case CardType.Draw4:
					return 50;
				default:
					return 0; // shouldn't happen
			}
		}

        private string buildImageString(CardColor color, CardType type)
        {
            string img = "";
            switch(color)
            {
                case CardColor.Blue:
                    img = "b";
                    break;
                case CardColor.Green:
                    img = "g";
                    break;
                case CardColor.Red:
                    img = "r";
                    break;
                case CardColor.Yellow:
                    img = "y";
                    break;
            }

            switch(type)
            {
                case CardType.Zero:
                    img += "0";
                    break;
                case CardType.One:
                    img += "1";
                    break;
                case CardType.Two:
                    img += "2";
                    break;
                case CardType.Three:
                    img += "3";
                    break;
                case CardType.Four:
                    img += "4";
                    break;
                case CardType.Five:
                    img += "5";
                    break;
                case CardType.Six:
                    img += "6";
                    break;
                case CardType.Seven:
                    img += "7";
                    break;
                case CardType.Eight:
                    img += "8";
                    break;
                case CardType.Nine:
                    img += "9";
                    break;
                case CardType.Skip:
                    img += "s";
                    break;
                case CardType.Reverse:
                    img += "r";
                    break;
                case CardType.Draw2:
                    img += "p";
                    break;
                case CardType.Wild:
                    img += "c";
                    break;
                case CardType.Draw4:
                    img += "p4";
                    break;
            }

            return img;
        }
    }
}
