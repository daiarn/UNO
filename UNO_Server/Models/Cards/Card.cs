using System;

namespace UNO.Models
{
	public enum CardColor
	{
		Black, Red, Yellow, Green, Blue
	}

	public enum CardType
	{
		Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Skip, Reverse, Draw2, Wild, Draw4
	}

    public class Card : IEquatable<Card>
    {
		public CardColor color;
		public CardType type;

		public Card(CardColor color, CardType type)
        {
            this.color = color;
            this.type = type;
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
    }
}
