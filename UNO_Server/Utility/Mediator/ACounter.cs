namespace UNO_Server.Utility.Mediator
{
	public abstract class ACounter
	{
		protected IMediator mediator;

		public ACounter(IMediator mediator = null)
		{
			this.mediator = mediator;
		}

		public void SetMediator(IMediator mediator)
		{
			this.mediator = mediator;
		}

	}
}
