namespace UNO_Server.Utility.Command
{
	public class Uno : ICommand
	{
		public void Execute()
		{
			Models.Game.GetInstance().PlayerSaysUNO();
		}

		public void Undo()
		{
			// uno has no undo
		}
	}
}
