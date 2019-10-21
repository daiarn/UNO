using System.Collections.Generic;

namespace UNO_Server.Models.SendData
{
	public class GamePlayerState : GameState
	{
		public List<Card> hand;
		public int id;

		public GamePlayerState(Game game, Player player) : base(game)
		{
			hand = player.hand.cards;
			id = game.GetPlayerIndexByUUID(player.id);
		}
	}
}
