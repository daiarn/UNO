using System;
using System.Collections.Generic;

namespace UNO_Server.Models.SendData
{
	public class GamePlayerState : GameSpectatorState
	{
		public List<Card> hand;
		public int index;

		public GamePlayerState(Game game, Guid id) : base(game)
		{
			var player = game.GetPlayerByUUID(id);

			hand = player.hand;
			index = System.Array.IndexOf(game.players, player);
		}
	}
}
