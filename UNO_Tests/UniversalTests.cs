using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
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
            
            Assert.IsInstanceOf<GameSpectatorState>(data["gamestate"]);

            Assert.IsNotNull(gamestate.discardPileCount);
            Assert.IsNotNull(gamestate.drawPileCount);

            Assert.IsNotNull(gamestate.activePlayer);
            Assert.IsNotNull(gamestate.players);
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

        [Test]
        public void TestGameControllerResults()
        {
            // ARRANGE
            var game = Game.ResetGame();
            var control = new GameController();

            var id = game.AddPlayer("Player One");
            game.AddPlayer("Player Two");
            game.players[0].isPlaying = true;
            game.players[1].isPlaying = true;

            game.phase = GamePhase.Playing;
            game.drawPile = new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.One));
            game.discardPile = new Deck();
            game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.One));
            game.activePlayerIndex = 0;
            game.observers[0].Counter = 4;
            game.observers[1].Counter = 0;

            var theCard = new Card(CardColor.Red, CardType.Zero);
            game.players[0].hand.Add(new Card(theCard));

            // ACT
            var response = control.Get(game.players[0].id) as JsonResult;
            var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];
            var gameState = (GamePlayerState)data["gamestate"];
            Assert.IsTrue(success);

            //Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
            Assert.AreEqual(game.observers[0].Counter, gameState.zeroCounter);
            Assert.AreEqual(game.observers[1].Counter, gameState.wildCounter);
            Assert.AreEqual(game.discardPile.GetCount(), gameState.discardPileCount);
            Assert.AreEqual(game.drawPile.GetCount(), gameState.drawPileCount);
            Assert.AreEqual(game.discardPile.PeekBottomCard(), gameState.activeCard);
            Assert.AreEqual(game.activePlayerIndex, gameState.activePlayer);
            Assert.AreEqual(game.players.Where(p => p != null).Select(p => new PlayerInfo(p)).ToList().Count(), gameState.players.Count());
        }
	}
}
