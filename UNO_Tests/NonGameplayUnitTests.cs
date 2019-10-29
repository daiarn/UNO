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
        public void TestJoinPhase1()
        {
            Game game = Game.GetInstance();
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
    }
}
