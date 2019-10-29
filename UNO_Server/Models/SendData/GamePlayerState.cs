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
			index = -1;
			for (int i = 0; i < game.numPlayers; i++)
			{
				if (game.players[i].id == player.id)
				{
					index = i;
					break;
				}
			}
		}
	}
}
