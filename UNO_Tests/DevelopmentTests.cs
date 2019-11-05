using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
	[TestFixture]
	public class DevelopmentTests
	{
		[Test]
		public void TestScenarioOne()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			// ACT
			control.Scenario(1);

			// ASSERT
			Assert.AreEqual(0, game.activePlayerIndex);
			Assert.AreEqual(GamePhase.Playing, game.phase);
		}
	}
}
