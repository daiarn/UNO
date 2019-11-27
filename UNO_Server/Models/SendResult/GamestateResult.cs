using UNO_Server.Models.SendData;

namespace UNO_Server.Models.SendResult
{
	public class GamestateResult : BaseResult
	{
		public readonly GameSpectatorState Gamestate;

		public GamestateResult(GameSpectatorState Gamestate) : base(true)
		{
			this.Gamestate = Gamestate;
		}
	}
}
