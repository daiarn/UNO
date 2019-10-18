using UNO.Models;

namespace UNO_Server.Utility
{
	abstract public class Factory
	{
		public abstract ICardStrategy CreateAction(CardType type);
	}
}