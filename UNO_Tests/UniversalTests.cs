using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.SendData;
using UNO_Server.Models.SendResult;

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
			var result = control.Get().Value as GamestateResult;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			var gamestate = result.Gamestate;
			Assert.IsNotNull(gamestate);

			Assert.AreEqual(0, gamestate.zeroCounter);
			Assert.AreEqual(0, gamestate.wildCounter);
			Assert.AreEqual(0, gamestate.discardPileCount);
			Assert.AreEqual(0, gamestate.drawPileCount);
			Assert.AreEqual(null, gamestate.activeCard);
			Assert.AreEqual(0, gamestate.activePlayer);
			Assert.AreEqual(0, gamestate.players.Count());
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
			var result = control.Get().Value as GamestateResult;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			var gamestate = result.Gamestate;
			Assert.IsNotNull(gamestate);

			Assert.AreEqual(0, gamestate.zeroCounter);
			Assert.AreEqual(0, gamestate.wildCounter);
			Assert.AreEqual(0, gamestate.discardPileCount);
			Assert.AreEqual(0, gamestate.drawPileCount);
			Assert.AreEqual(null, gamestate.activeCard);
			Assert.AreEqual(0, gamestate.activePlayer);
			Assert.AreEqual(2, gamestate.players.Count());
		}

		[Test]
		public void TestPlayerNotInGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			// ACT
			var result = control.Get(new System.Guid()).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			System.Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void TestPlayerTwoPlayerGame() // TODO: add some cards to player hands
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			// ACT
			var result = control.Get(id).Value as GamestateResult;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			var gamestate = (GamePlayerState) result.Gamestate;
			Assert.IsNotNull(gamestate);

			Assert.AreEqual(0, gamestate.zeroCounter);
			Assert.AreEqual(0, gamestate.wildCounter);
			Assert.AreEqual(0, gamestate.discardPileCount);
			Assert.AreEqual(0, gamestate.drawPileCount);
			Assert.AreEqual(null, gamestate.activeCard);
			Assert.AreEqual(0, gamestate.activePlayer);
			Assert.AreEqual(2, gamestate.players.Count());

			// more gamestate checks
			Assert.IsNotNull(gamestate.hand);
			Assert.AreEqual(0, gamestate.hand.Count());
		}

		[Test]
		public void TestGameControllerAllResults()
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
			game.gameWatcher.observers[0].Counter = 4;
			game.gameWatcher.observers[1].Counter = 0;

			game.players[0].hand.Add(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var result = control.Get(id).Value as GamestateResult;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			var gamestate = (GamePlayerState) result.Gamestate; // TODO: casting is not necessary, add some stuff for that player
			Assert.IsNotNull(gamestate);

			Assert.AreEqual(4, gamestate.zeroCounter);
			Assert.AreEqual(0, gamestate.wildCounter);
			Assert.AreEqual(1, gamestate.discardPileCount);
			Assert.AreEqual(1, gamestate.drawPileCount);
			Assert.AreEqual(new Card(CardColor.Red, CardType.One), gamestate.activeCard);
			Assert.AreEqual(0, gamestate.activePlayer);
			Assert.AreEqual(2, gamestate.players.Count());
		}
	}
}
