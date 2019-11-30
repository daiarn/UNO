using UNO_Server.Models;
using UNO_Server.Models.SendResult;

namespace UNO_Server.ChainOfResponsibility
{
	public abstract class ConditionChain
	{
		protected ConditionChain next;
		public ConditionChain Then(ConditionChain condition)
		{
			var last = this;
			while (last.next != null)
				last = last.next;

			last.next = condition;
			return this;
		}

		public abstract BaseResult ProcessChain(Game game);
	}
}
