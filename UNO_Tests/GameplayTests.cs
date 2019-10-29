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
		public void TestPlayCardPreGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			//game.AddPlayer();

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
		public void TestPlayCardNoPlayerGame()
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
	}
}
