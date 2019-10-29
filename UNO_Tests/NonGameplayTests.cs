using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;

namespace UNO_Tests
{
	[TestClass]
	public class NonGameplayTests
	{
		[TestMethod]
		public void TestJoinSuccess()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			JoinData joinData = new JoinData { name = "My name" };

			var response = control.Join(joinData) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(1, game.GetActivePlayerCount());
		}
		[TestMethod]
		public void TestJoinFailPhase1() // TODO: refactor for all phases
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			JoinData joinData = new JoinData { name = "My name" };
			game.phase = GamePhase.Playing;

			var response = control.Join(joinData) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);

			Assert.AreEqual(0, game.GetActivePlayerCount());
		}

		[TestMethod]
		public void TestLeaveGame() // TODO: more than one scenario
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			var player = game.AddPlayer("Player One");
			game.phase = GamePhase.Playing;

			var response = control.Leave(new PlayerData { id = player }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(0, game.GetActivePlayerCount());
		}

		[TestMethod]
		public void TestStartAlreadyStarted()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			var player = game.AddPlayer("Player One");
			game.phase = GamePhase.Finished;

			var response = control.Start(new StartData { id = player, finiteDeck = false, onlyNumbers = false }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestStartNotInGame()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			game.phase = GamePhase.WaitingForPlayers;

			var response = control.Start(new StartData { id = new Guid(), finiteDeck = false, onlyNumbers = false }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);

			Assert.AreEqual(game.phase, GamePhase.WaitingForPlayers);
		}
	}
}
