using UNO_Client.Forms;

namespace UNO_Client.State
{
	public class LosingState : PlayerState
	{
		public void WritePlayerStatusInGame(GameForm form, StateContext context)
		{
			form.ChangeLabeToLost();
		}
	}
}
