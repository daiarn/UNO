using System;
using System.Collections.Generic;

namespace UNO_Server.Models.SendData
{
	public class GamePlayerState : GameSpectatorState
	{
		public List<Card> hand;
		public int index;
		public int scoreboardIndex;

		public GamePlayerState(Game game, Guid id) : base(game)
		{
			var player = game.GetPlayerByUUID(id);

			hand = player.hand;
			index = System.Array.IndexOf(game.players, player);

			scoreboardIndex = -1;
            if (game.scoreboard != null)
			    for (int i = 0; i < game.scoreboard.Length; i++)
				    if (game.scoreboard[i] != null && game.scoreboard[i].index == index)
					    scoreboardIndex = i;
		}
	}
}
