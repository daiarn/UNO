using System;

namespace UNO_Server.Models.SendResult
{
	public class JoinResult : BaseResult
	{
		private readonly Guid id;
		public JoinResult(Guid id) : base(true)
		{
			this.id = id;
		}
	}
}
