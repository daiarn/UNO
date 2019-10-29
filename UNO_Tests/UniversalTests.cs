using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
	[TestClass]
	public class UniversalTests
	{
		[TestMethod]
		public void TestNothing()
		{
			Game.GetInstance();
		}

		[TestMethod]
		public void TestGetSpectatorEmptyGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
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

		[TestMethod]
		public void TestGetSpectatorTwoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			// ACT
			var response = control.Get() as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			var gamestate = new Microsoft.AspNetCore.Routing.RouteValueDictionary(data["gamestate"]);

			Assert.IsTrue(success);

			//System.Console.WriteLine(data["gamestate"].ToString()); // TODO: refactor test, check relevant values?
			Assert.IsNotNull(gamestate);

			Assert.IsNotNull(gamestate["discardPile"]);
			Assert.IsNotNull(gamestate["drawPile"]);
			//Assert.IsNull(gamestate["activeCard"]); // no card in pre-game phase

			Assert.IsNotNull(gamestate["activePlayer"]);
			Assert.IsNotNull(gamestate["players"]);
		}

		[TestMethod]
		public void TestGetPlayerNotInGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			// ACT
			var response = control.Get(new System.Guid()) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestGetPlayerTwoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			// ACT
			var response = control.Get(id) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			var gamestate = new Microsoft.AspNetCore.Routing.RouteValueDictionary(data["gamestate"]);

			Assert.IsTrue(success);
			Assert.IsNotNull(gamestate);
			//System.Console.WriteLine(data["gamestate"].ToString());

			Assert.IsNotNull(gamestate["discardPile"]);
			Assert.IsNotNull(gamestate["drawPile"]);

			Assert.IsNotNull(gamestate["activePlayer"]);
			Assert.IsNotNull(gamestate["players"]);

			var hand = (List<Card>) gamestate["hand"];
			Assert.IsNotNull(hand);
			System.Console.WriteLine(hand);
			System.Console.WriteLine(hand.Count);

			// TODO: check cards in hand
		}
	}
}
