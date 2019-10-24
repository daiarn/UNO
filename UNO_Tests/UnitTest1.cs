using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
	[TestClass]
	public class GameplayTests
	{
		[TestMethod]
		public void TestGetSpectator()
		{
			// ARRANGE
			var game = Game.GetInstance();
			var control = new GameController();

			// ACT
			var response = control.Get() as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			var gamestate = new Microsoft.AspNetCore.Routing.RouteValueDictionary(data["gamestate"]);

			//foreach (var key in gamestate.Keys)
				//System.Console.WriteLine(key + ": " + gamestate[key]);

			Assert.IsTrue(success);
			Assert.IsNotNull(gamestate);

			Assert.IsNotNull(gamestate["discardPile"]);
			Assert.IsNotNull(gamestate["drawPile"]);

			Assert.IsNotNull(gamestate["activePlayer"]);
			Assert.IsNotNull(gamestate["players"]);
		}
	}
}
