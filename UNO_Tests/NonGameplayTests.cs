using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;

namespace UNO_Tests
{
	[TestFixture]
	public class NonGameplayTests
	{
		[Test]
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
		[Test]
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

		[Test]
		public void TestJoinTenthPlayer()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.AddPlayer("Player Four");
			game.AddPlayer("Player Five");
			game.AddPlayer("Player Six");
			game.AddPlayer("Player Seven");
			game.AddPlayer("Player Eight");
			game.AddPlayer("Player Nine");

			JoinData joinData = new JoinData { name = "My name" };

			var response = control.Join(joinData) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(10, game.GetActivePlayerCount());
		}

		[Test]
		public void TestJoinEleventhPlayer()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.AddPlayer("Player Four");
			game.AddPlayer("Player Five");
			game.AddPlayer("Player Six");
			game.AddPlayer("Player Seven");
			game.AddPlayer("Player Eight");
			game.AddPlayer("Player Nine");
			game.AddPlayer("Player Ten");

			JoinData joinData = new JoinData { name = "My name" };

			var response = control.Join(joinData) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);

			Assert.AreEqual(10, game.GetActivePlayerCount());
		}

		[Test]
		public void TestLeaveGame() // TODO: more than one scenario
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			var player = game.AddPlayer("Player One");
			game.phase = GamePhase.WaitingForPlayers;

            var response = control.Leave(new PlayerData { id = player }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(0, game.GetActivePlayerCount());
		}

		[Test]
		public void TestLeaveMidGame() // TODO: maybe add with the other ones
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

		[Test]
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

		[Test]
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

		[Test]
		//[ExpectedException(typeof(NotImplementedException), "Game over, go home")]
		public void TestGameOver()
		{
			Game game = Game.ResetGame();

			//game.GameOver();
            var ex = Assert.Throws(typeof(NotImplementedException), new TestDelegate(game.GameOver));
            Assert.That(ex.Message == "Game over, go home");
        }

		[Test]
		public void TestStartClassicSuccess()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.WaitingForPlayers;

			var response = control.Start(new StartData { id = id, finiteDeck = false, onlyNumbers = false }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			//Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(game.phase, GamePhase.Playing);
		}

		[Test]
		public void TestStartFiniteNumbersOnlySuccess()
		{
			Game game = Game.ResetGame();
			GameController control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.WaitingForPlayers;

			var response = control.Start(new StartData { id = id, finiteDeck = true, onlyNumbers = true }) as JsonResult;
			var data = new RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			//Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.AreEqual(game.phase, GamePhase.Playing);
		}
	}
}
