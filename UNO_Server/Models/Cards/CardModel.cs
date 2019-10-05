using System;

namespace UNO.Models
{
	public enum CardValue
	{
		One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Zero, Skip, Reverse, Draw2, Wild, Draw4
	}

    public class CardModel : Card
    {
        //public Color color { get; set; }
        public CardValue value { get; set; }

		public CardModel(Color color, CardValue value) : base(color)
        {
            this.color = color;
            this.value = value;
        }

		public int getScore()
		{
			switch (value)
			{
				case CardValue.Zero:
					return 0;
				case CardValue.One:
					return 1;
				case CardValue.Two:
					return 2;
				case CardValue.Three:
					return 3;
				case CardValue.Four:
					return 4;
				case CardValue.Five:
					return 5;
				case CardValue.Six:
					return 6;
				case CardValue.Seven:
					return 7;
				case CardValue.Eight:
					return 8;
				case CardValue.Nine:
					return 9;
				case CardValue.Skip:
				case CardValue.Reverse:
				case CardValue.Draw2:
					return 20;
				case CardValue.Wild:
				case CardValue.Draw4:
					return 50;
				default:
					return 0; // shouldn't happen
			}
		}
    }
}
