using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
	[TestClass]
	public class DevelopmentTests
	{

		[TestMethod]
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
