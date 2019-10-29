using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UNO_Server.Controllers;
using UNO_Server.Models;

namespace UNO_Tests
{
    [TestClass]
    public class NonGameplayUnitTests
    {
        [TestMethod]
        public void TestJoinSuccess()
        {
            Game game = Game.ResetGame();
            GameController control = new GameController();

            JoinData joinData = new JoinData
            {
                name = "my name"
            };

            var response = control.Join(joinData) as JsonResult;
            var data = new RouteValueDictionary(response.Value);

            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];

            Assert.IsTrue(success);
        }
        [TestMethod]
        public void TestJoinFailPhase1() // TODO: refactor for all phases
        {
            Game game = Game.ResetGame();
            GameController control = new GameController();

            JoinData joinData = new JoinData
            {
                name = "my name"
            };

            game.phase = GamePhase.Playing;
            var response = control.Join(joinData) as JsonResult;
            var data = new RouteValueDictionary(response.Value);
            Console.WriteLine(data["message"]);
            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TestLeaveGame()
        {
            Game game = Game.ResetGame();
            GameController control = new GameController();

            JoinData joinData = new JoinData
            {
                name = "my name"
            };
            var player = game.AddPlayer("Player one");
            game.phase = GamePhase.Playing;
            var response = control.Leave(new PlayerData { id = player }) as JsonResult;
            var data = new RouteValueDictionary(response.Value);
            Console.WriteLine(data["message"]);
            
            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestStartGameAlreadyStarted()
        {
            Game game = Game.ResetGame();
            GameController control = new GameController();

            var player = game.AddPlayer("Player one");
            game.phase = GamePhase.Finished;
            var response = control.Start(new StartData { id = player, finiteDeck = false, onlyNumbers = false}) as JsonResult;
            var data = new RouteValueDictionary(response.Value);
            Console.WriteLine(data["message"]);

            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TestStartGameNotInTheGame()
        {
            Game game = Game.ResetGame();
            GameController control = new GameController();

            game.phase = GamePhase.WaitingForPlayers;
            var response = control.Start(new StartData { id = new System.Guid(), finiteDeck = false, onlyNumbers = false }) as JsonResult;
            var data = new RouteValueDictionary(response.Value);
            Console.WriteLine(data["message"]);

            // ASSERT
            Assert.IsNotNull(response);

            var success = (bool)data["success"];
            Assert.IsFalse(success);
        }
    }
}
