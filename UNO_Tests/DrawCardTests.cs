using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UNO_Server.Controllers;
using UNO_Server.Models;

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
            var pl = game.AddPlayer("Player one");
            game.phase = GamePhase.Playing;
            game.drawPile =new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
            var response = control.Draw(new PlayerData { id = pl }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);
            System.Console.WriteLine(data["message"]);

            var success = (bool)data["success"];
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestGameIsntStarted()
		{
			// ARRANGE
			var game = Game.ResetGame();
            var control = new GameController();
            var pl = game.AddPlayer("Player one");
            game.phase = GamePhase.WaitingForPlayers;
            //game.drawPile = new Deck();
            //game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
            var response = control.Draw(new PlayerData { id = pl }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);
            System.Console.WriteLine(data["message"]);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TestNotInGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
            var control = new GameController();
            var pl = game.AddPlayer("Player one");
            game.phase = GamePhase.Playing;
            game.drawPile = new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
            var response = control.Draw(new PlayerData { id = new System.Guid() }) as JsonResult;
            var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);
            System.Console.WriteLine(data["message"]);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TestNotYourTurn()
		{
			// ARRANGE
			var game = Game.ResetGame();
            var control = new GameController();
            var pl1 = game.AddPlayer("Player one");
            var pl2 = game.AddPlayer("Player two");
            game.activePlayerIndex = 0;
            game.phase = GamePhase.Playing;
            game.drawPile = new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Draw(new PlayerData { id = pl2 }) as JsonResult;
            var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);
            System.Console.WriteLine(data["message"]);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TestCantDraw() // TODO: this test should probably change after ExpectedAction refactor
        {
			// ARRANGE
            var game = Game.ResetGame();
            var control = new GameController();
            var pl1 = game.AddPlayer("Player one");
            var pl2 = game.AddPlayer("Player two");

            game.activePlayerIndex = 1;
            game.expectedAction = ExpectedPlayerAction.PlayCard;
            game.phase = GamePhase.Playing;
            game.drawPile = new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
            var response = control.Draw(new PlayerData { id = pl2 }) as JsonResult;
            var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);
            System.Console.WriteLine(data["message"]);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }
    }
}
