using System;

namespace UNO.Models
{
	class NumberCard : Card
    {
        private int number { get; set; }

        public NumberCard(Color color, int number) : base(color)
        {
            this.number = number;
        }
    }
}
