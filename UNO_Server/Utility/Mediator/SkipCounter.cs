﻿using UNO_Server.Models;

namespace UNO_Server.Utility.Mediator
{
	public class SkipCounter : ACounter
	{
		public void Count(Game game)
		{
			game.skipCount++;
		}
	}
}
