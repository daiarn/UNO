using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
using UNO_Server.Models.SendResult;

namespace UNO_Tests
{
	[TestFixture]
	public class NonGameplayTests
	{
		private Game game;
		private GameController control;

		[SetUp]
		public void TestInit()
		{
			game = Game.ResetGame();
			control = new GameController();
		}

		[Test]
		public void TestJoinSuccess()
		{
			JoinData joinData = new JoinData { name = "My name" };
			var result = control.Join(joinData).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			Assert.AreEqual(1, game.GetActivePlayerCount());
		}

		[TestCase(GamePhase.Playing)]
		[TestCase(GamePhase.Finished)]
		[Test]
		public void TestJoinPhase(GamePhase gamePhase)
		{
			game.phase = gamePhase;

			JoinData joinData = new JoinData { name = "My name" };
			var result = control.Join(joinData).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(0, game.GetActivePlayerCount());
		}

		[Test]
		public void TestJoinTenthPlayer()
		{
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
			var result = control.Join(joinData).Value;

			// ASSERT
			Assert.IsNotNull(result);

			var success = result.Success;
			Assert.IsTrue(success);

			Assert.AreEqual(10, game.GetActivePlayerCount());
		}

		[Test]
		public void TestJoinEleventhPlayer()
		{
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
			var result = control.Join(joinData).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(10, game.GetActivePlayerCount());
		}

		[TestCase(GamePhase.WaitingForPlayers)]
		[TestCase(GamePhase.Playing)]
		[TestCase(GamePhase.Finished)]
		[Test]
		public void TestLeaveGame(GamePhase gamePhase)
		{
			var player = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;
			game.players[2].isPlaying = true;

			game.slowGame = true;
			game.phase = gamePhase;
			game.scoreboard = new UNO_Server.Models.SendData.ScoreboardInfo[3];

			var result = control.Leave(new PlayerData { id = player }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			//Console.WriteLine(result.Message);
			Assert.IsTrue(result.Success);

			Console.WriteLine(game.phase);

			Assert.AreEqual(2, game.GetActivePlayerCount());
		}

		[Test]
		public void TestStartAlreadyStarted()
		{
			var player = game.AddPlayer("Player One");
			game.phase = GamePhase.Finished;

			var result = control.Start(new StartData { id = player, finiteDeck = false, onlyNumbers = false }).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void TestStartNotInGame()
		{
			game.phase = GamePhase.WaitingForPlayers;

			var result = control.Start(new StartData { id = new Guid(), finiteDeck = false, onlyNumbers = false }).Value as FailResult;

			// ASSERT
			Assert.IsNotNull(result);
			Console.WriteLine(result.Message);
			Assert.IsFalse(result.Success);

			Assert.AreEqual(game.phase, GamePhase.WaitingForPlayers);
		}

		[Test]
		public void TestStartClassicSuccess()
		{
			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.WaitingForPlayers;

			var result = control.Start(new StartData { id = id, finiteDeck = false, onlyNumbers = false }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			Assert.AreEqual(game.phase, GamePhase.Playing);
		}

		[Test]
		public void TestStartFiniteNumbersOnlySuccess()
		{
			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.phase = GamePhase.WaitingForPlayers;

			var result = control.Start(new StartData { id = id, finiteDeck = true, onlyNumbers = true }).Value;

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);

			Assert.AreEqual(game.phase, GamePhase.Playing);
		}
	}
}
