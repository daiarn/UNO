using NUnit.Framework;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
using UNO_Server.Models.SendResult;

namespace UNO_Tests
{
	[TestFixture]
	public class DrawCardTests
	{
		[Test]
		public void TestInfinite()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.finiteDeck = false;
			game.phase = GamePhase.Playing;
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.One));
			game.cardsCounter = new CardsCounter(game.players);

			// ACT
			var result = control.Draw(new PlayerData { id = id }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result);
			Assert.IsTrue(result.Success);

			Assert.AreEqual(1, game.players[0].hand.Count);
			Assert.IsTrue(game.players[0].hand.Contains(new Card(CardColor.Red, CardType.Zero)));
		}

		[Test]
		public void TestFinite()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.finiteDeck = true;
			game.phase = GamePhase.Playing;
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.cardsCounter = new CardsCounter(game.players);

			// ACT
			var result = control.Draw(new PlayerData { id = id }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			//Console.WriteLine(result.Message);
			Assert.IsTrue(result.Success);

			Assert.AreEqual(1, game.players[0].hand.Count);
			Assert.IsTrue(game.players[0].hand.Contains(new Card(CardColor.Red, CardType.Zero)));
		}

		[Test]
		public void TestGameIsntStarted()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.phase = GamePhase.WaitingForPlayers;

			// ACT
			var result = control.Draw(new PlayerData { id = id }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result);

			var success = result.Success;
			Assert.IsFalse(success);

			Assert.AreEqual(0, game.players[0].hand.Count);
		}

		[Test]
		public void TestNotInGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;

			// ACT
			var result = control.Draw(new PlayerData { id = new Guid() }).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(0, game.players[0].hand.Count);
		}

		[Test]
		public void TestNotYourTurn()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.Playing;
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.activePlayerIndex = 1;

			// ACT
			var result = control.Draw(new PlayerData { id = new Guid() }).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(0, game.players[0].hand.Count);
		}

		[Test]
		public void TestCantDraw()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			game.phase = GamePhase.Playing;
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
			game.activePlayerIndex = 0;
			game.players[0].hand.Add(new Card(CardColor.Black, CardType.Wild));

			// ACT
			var result = control.Draw(new PlayerData { id = new Guid() }).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(1, game.players[0].hand.Count);
			Assert.IsFalse(game.players[0].hand.Contains(new Card(CardColor.Red, CardType.Zero)));
		}
	}
}
