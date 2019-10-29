using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
	[TestClass]
	public class PlayCardTests
	{
		[TestMethod]
		public void TestPreGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			// ACT
			var response = control.Play(new PlayData { id = new System.Guid(), color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestNoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.phase = GamePhase.Playing;
			game.discardPile = new Deck();
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = new System.Guid(), color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestWrongTurn()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.phase = GamePhase.Playing;
			game.discardPile = new Deck();
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.activePlayerIndex = 1;

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestPlayerDoesntHaveCard()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;
			game.discardPile = new Deck();
			game.discardPile.AddToBottom(new Card(CardColor.Yellow, CardType.One));
			game.activePlayerIndex = 0;

			game.expectedAction = ExpectedPlayerAction.PlayCard; // TODO: remove this after refactor

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestCardDoesntMatch()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;
			game.discardPile = new Deck();
			game.discardPile.AddToBottom(new Card(CardColor.Yellow, CardType.One));
			game.activePlayerIndex = 0;
			game.expectedAction = ExpectedPlayerAction.PlayCard; // TODO: remove this after refactor

			game.players[0].hand.Add(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			System.Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void TestSuccess()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;
			game.discardPile = new Deck();
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.One));
			game.activePlayerIndex = 0;
			game.expectedAction = ExpectedPlayerAction.PlayCard; // TODO: remove this after refactor

			game.players[0].hand.Add(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = game.players[0].id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);
		}
	}
}
