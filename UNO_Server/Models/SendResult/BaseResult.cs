namespace UNO_Server.Models.SendResult
{
	public class BaseResult
	{
		public readonly bool Success;

		protected BaseResult(bool Success)
		{
			this.Success = Success;
		}
		public BaseResult()
		{
			Success = true;
		}
	}
}
