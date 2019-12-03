using System;

namespace UNO_Client.Models
{
	public class Card : IEquatable<Card>
	{
		int color;
		int type;

		public Card(int color, int type)
		{
			this.color = color;
			this.type = type;
		}

		public int Color { get => color; set => color = value; }
		public int Type { get => type; set => type = value; }

		public bool Equals(Card other)
		{
			return color == other.color && type == other.type;
		}
		public override int GetHashCode()
		{
			return color.GetHashCode() & type.GetHashCode();
		}
		/*
		public static bool operator ==(Card left, Card right)
		{
			if (left == null) return false;
			return left.Equals(right);
		}
		public static bool operator !=(Card left, Card right)
		{
			if (left == null) return false;
			return !left.Equals(right);
		}//*/
	}
}
