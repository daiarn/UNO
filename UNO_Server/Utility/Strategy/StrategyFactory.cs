using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
{
	abstract public class StrategyFactory
	{
		public abstract ICardStrategy CreateAction(CardType type);
	}
}