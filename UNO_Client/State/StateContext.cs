using UNO_Client.Forms;

namespace UNO_Client.State
{
	public class StateContext
	{
		private PlayerState state;

		public StateContext()
		{
			state = new WaitingState();
		}

		public void setState(PlayerState newState)
		{
			state = newState;
		}

		public void WritePlayerStatusInGame(GameForm form)
		{
			state.WritePlayerStatusInGame(form, this);
		}
	}
}
