using System.Collections.Generic;

namespace UNO_Server.Models.SendData
{
	public class GamePlayerState : GameSpectatorState
	{
		public List<Card> hand;
		public int index;

		public GamePlayerState(Game game, Player player) : base(game)
		{
			hand = player.hand;
			index = game.GetPlayerIndexByUUID(player.id);
		}
	}
}
