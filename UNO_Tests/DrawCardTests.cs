using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;

namespace UNO_Tests
{
	[TestClass]
	public class DrawCardTests
	{
		[TestMethod]
		public void TestSuccess()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player one");
			game.phase = GamePhase.Playing;
			game.drawPile = new Deck();
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Draw(new PlayerData { id = id }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);
		}

		[TestMethod]
		public void TestGameIsntStarted()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player one");
			game.phase = GamePhase.WaitingForPlayers;

			// ACT
			var response = control.Draw(new PlayerData { id = id }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestNotInGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;

			// ACT
			var response = control.Draw(new PlayerData { id = new System.Guid() }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestNotYourTurn()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player one");
			game.AddPlayer("Player two");
			game.phase = GamePhase.Playing;
			game.drawPile = new Deck();
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.activePlayerIndex = 1;

			// ACT
			var response = control.Draw(new PlayerData { id = id }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestCantDraw()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player one");
			game.AddPlayer("Player two");

			game.phase = GamePhase.Playing;
			game.drawPile = new Deck();
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.activePlayerIndex = 0;
			game.players[0].hand.Add(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Draw(new PlayerData { id = id }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}
	}
}
