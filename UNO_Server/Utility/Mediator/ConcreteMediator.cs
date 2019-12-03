using System.Linq;
using UNO_Server.Models;

namespace UNO_Server.Utility.Mediator
{
	public class ConcreteMediator : IMediator
	{
		private CardCounter cardCounter;
		private MoveCounter moveCounter;
		private SkipCounter skipCounter;

		private Game game;

		public ConcreteMediator(Game game, CardCounter cardCounter, MoveCounter moveCounter, SkipCounter skipCounter)
		{
			this.skipCounter = skipCounter;
			this.cardCounter = cardCounter;
			this.moveCounter = moveCounter;
			this.game = game;
		}

		public void Notify(string ev)
		{
			if (ev == "card")
			{
				cardCounter.Count(game);
				moveCounter.Count(game);
			}

			if (ev == "move")
			{
				moveCounter.Count(game);
			}

			if (ev == "skip")
			{
				skipCounter.Count(game);
				moveCounter.Count(game);
				cardCounter.Count(game);
			}
		}
	}
}
