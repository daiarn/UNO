using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.SendData;

namespace UNO_Tests
{
	[TestFixture]
	public class UniversalTests
	{
		[Test]
		public void TestSingleton()
		{
			var game = Game.GetInstance();
            Assert.AreSame(game, Game.GetInstance());
		}

		[Test]
		public void TestSpectatorEmptyGame()
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
			var gamestate = (GameSpectatorState) data["gamestate"];

			Assert.IsTrue(success);
			Assert.IsNotNull(gamestate);

			//Assert.IsInstanceOfType(data["gamestate"], new GameSpectatorState(game).GetType());

			// TODO: refactor these?
			//Assert.IsNotNull(gamestate.discardPileCount);
			//Assert.IsNotNull(gamestate.drawPileCount);

			//Assert.IsNotNull(gamestate.activePlayer);
			//Assert.IsNotNull(gamestate.players);
		}

		[Test]
		public void TestSpectatorTwoPlayerGame()
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
			var gamestate = (GameSpectatorState) data["gamestate"];

			Assert.IsTrue(success);
			Assert.IsNotNull(gamestate);
		}

		[Test]
		public void TestPlayerNotInGame()
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

		[Test]
		public void TestPlayerTwoPlayerGame()
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

			var success = (bool) data["success"];
			var gamestate = (GamePlayerState) data["gamestate"];

			Assert.IsTrue(success);
			Assert.IsNotNull(gamestate);

			// maybe more gamestate checks?

			var hand = (List<Card>) gamestate.hand;
			Assert.IsNotNull(hand);
			System.Console.WriteLine(hand);
			System.Console.WriteLine(hand.Count);

			// TODO: check cards in hand
		}
	}
}
