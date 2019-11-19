using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;

namespace UNO_Tests
{
	[TestFixture]
	public class PlayCardTests
	{
		[Test]
		public void TestPreGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			// ACT
			var response = control.Play(new PlayData { id = new Guid(), color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[Test]
		public void TestNoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			game.phase = GamePhase.Playing;
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = new Guid(), color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[Test]
		public void TestWrongTurn()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 1;
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[Test]
		public void TestPlayerDoesntHaveCard()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.discardPile.AddToBottom(new Card(CardColor.Yellow, CardType.One));

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[Test]
		public void TestCardDoesntMatch()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.discardPile.AddToBottom(new Card(CardColor.Yellow, CardType.One));

			game.players[0].hand.Add(new Card(CardColor.Red, CardType.Zero));

			// ACT
			var response = control.Play(new PlayData { id = id, color = CardColor.Red, type = CardType.Zero }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);
		}

		[Test]
		public void TestNumberCard()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Zero);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.discardPile.AddToBottom(new Card(theCard));

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
		}

		[Test]
		public void TestColorlessWild()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.discardPile.AddToBottom(new Card(CardColor.Red, CardType.One));

			var theCard = new Card(CardColor.Black, CardType.Wild); // wild cards
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsFalse(success);

			Assert.IsTrue(game.players[0].hand.Contains(theCard));
		}

		[Test]
		public void TestSkipTwoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Skip);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreEqual(0, game.activePlayerIndex);
		}

		[Test]
		public void TestSkipThreePlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Skip);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;
			game.players[2].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreEqual(2, game.activePlayerIndex);
		}

		[Test]
		public void TestReverseTwoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Reverse);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;

			game.flowClockWise = true;
			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreEqual(true, game.flowClockWise);
			Assert.AreEqual(0, game.activePlayerIndex);
		}

		[Test]
		public void TestReverseThreePlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Reverse);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;
			game.players[2].isPlaying = true;

			game.flowClockWise = true;
			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreNotEqual(true, game.flowClockWise);
			Assert.AreEqual(2, game.activePlayerIndex);
		}

		[Test]
		public void TestDraw2TwoPlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Draw2);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.One));
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.One));

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));
			game.cardsCounter = new CardsCounter(game.players);

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreEqual(2, game.players[1].hand.Count);
			Assert.AreEqual(0, game.activePlayerIndex);
		}

		[Test]
		public void TestDraw2ThreePlayerGame()
		{
			// ARRANGE
			var game = Game.ResetGame();
			var control = new GameController();

			var theCard = new Card(CardColor.Red, CardType.Draw2);

			var id = game.AddPlayer("Player One");
			game.AddPlayer("Player Two");
			game.AddPlayer("Player Three");
			game.players[0].isPlaying = true;
			game.players[1].isPlaying = true;
			game.players[2].isPlaying = true;

			game.phase = GamePhase.Playing;
			game.activePlayerIndex = 0;
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.One));
			game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.One));

			game.players[0].hand.Add(new Card(theCard));
			game.players[0].hand.Add(new Card(theCard));
			game.cardsCounter = new CardsCounter(game.players);

			// ACT
			var response = control.Play(new PlayData { id = id, color = theCard.color, type = theCard.type }) as JsonResult;
			var data = new Microsoft.AspNetCore.Routing.RouteValueDictionary(response.Value);

			// ASSERT
			Assert.IsNotNull(response);
			Console.WriteLine(data["message"]);

			var success = (bool) data["success"];
			Assert.IsTrue(success);

			Assert.IsTrue(theCard.Equals(game.discardPile.PeekBottomCard()));
			Assert.AreEqual(2, game.players[1].hand.Count);
			Assert.AreEqual(2, game.activePlayerIndex);
		}

		// TODO: more tests for draw4 card
	}
}
