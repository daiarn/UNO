using UNO_Client.Forms;

namespace UNO_Client.State
{
	public interface PlayerState
	{
		void WritePlayerStatusInGame(GameForm form, StateContext context);
	}
}
