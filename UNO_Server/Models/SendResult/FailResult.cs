namespace UNO_Server.Models.SendResult
{
	public class FailResult : BaseResult
	{
		public readonly string Message;

		public FailResult(string Message) : base(false)
		{
			this.Message = Message;
		}
	}
}
