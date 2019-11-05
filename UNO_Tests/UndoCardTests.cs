using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System;
using UNO_Server.Controllers;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
namespace UNO_Tests
{
    [TestFixture]
    public class UndoCardTests
    {
        [Test]
        public void TestUndoDrawCard()
        {
            // ARRANGE
            var game = Game.ResetGame();
            var control = new GameController();

            game.AddPlayer("Player One");
            game.AddPlayer("Player Two");
            game.phase = GamePhase.Playing;
            game.drawPile = new Deck();
            game.drawPile.AddToBottom(new Card(CardColor.Red, CardType.Zero));
            game.activePlayerIndex = 0;
            game.players[0].hand.Add(new Card(CardColor.Black, CardType.Wild));

            // ASSERT
            game.UndoDrawCard(0);

            Assert.AreEqual(0, game.players[0].hand.Count);
            Assert.AreEqual(2, game.drawPile.GetCount());
        }
    }
}
