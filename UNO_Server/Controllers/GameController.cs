using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO_Server.Models;
using UNO_Server.Utility.Command;

namespace UNO_Server.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
	{
        DrawCard drawCard = new DrawCard();
        Uno uno = new Uno();

        #region UNIVERSAL

		    /// <summary>
		    /// GET api/game
		    /// Gets the state of the game from a spectator's point of view
		    /// </summary>
		    /// <returns>Game state</returns>
		    [HttpGet]
		    public ActionResult Get()
		    {
			    var game = Game.GetInstance();

			    List<object> allPlayerData = new List<object>();
			    foreach (var player in game.players)
			    {
				    if (player == null) break;
				    allPlayerData.Add(new { player.name, count = player.hand.GetCount(), player.isPlaying });
			    }

			    return new JsonResult(new
			    {
				    success = true,
				    gamestate = new
				    {
					    zeroCounter = game.observers[0].Counter,
					    wildCounter = game.observers[1].Counter,

					    discardPile = game.discardPile.GetCount(),
					    drawPile = game.drawPile.GetCount(),
					    activeCard = game.discardPile.PeekBottomCard(),

					    activePlayer = game.activePlayerIndex,
					    players = allPlayerData
				    }
			    });
		    }

		    /// <summary>
		    /// GET api/game/5
		    /// Gets the state of the game from a player's point of view
		    /// </summary>
		    /// <param name="id">Player authentification identifier</param>
		    /// <returns>Game state</returns>
		    [HttpGet("{id}")]
		    public ActionResult Get(Guid id)
		    {
			    var game = Game.GetInstance();
			    var player = game.GetPlayerByUUID(id);

			    if (player == null)
			    {
				    return new JsonResult(new
				    {
					    success = false,
					    message = "You are not in the game"
				    });
			    }

			    List<object> allPlayerData = new List<object>();
			    foreach (var item in game.players)
			    {
				    if (item == null) break;
				    allPlayerData.Add(new { name = item.name, count = item.hand.GetCount(), isPlaying = item.isPlaying });
			    }


			    return new JsonResult(new
			    {
				    success = true,
				    gamestate = new
				    {
					    zeroCounter = game.observers[0].Counter,
					    wildCounter = game.observers[1].Counter,

					    discardPile = game.discardPile.GetCount(),
					    drawPile = game.drawPile.GetCount(),
					    activeCard = game.discardPile.PeekBottomCard(),

					    activePlayer = game.activePlayerIndex,
					    players = allPlayerData,

					    hand = player.hand.cards
				    }
			    });
		    }

		    #endregion

		#region NON-GAMEPLAY
		
		/// <summary>
		/// POST api/game/join
		/// A new player attempts to join the game
		/// </summary>
		/// <param name="data">Player information</param>
		/// <returns>Response message</returns>
		[HttpPost("join")]
		public ActionResult Join(JoinData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.WaitingForPlayers)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game already started"
				});
			}
			else if (game.GetActivePlayerCount() >= 10)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game player capacity exceeded"
				});
			}

			Guid id = game.AddPlayer(data.name);
			return new JsonResult(new { success = true, id = id });
		}

		/// <summary>
		/// POST api/game/leave
		/// Player leaves or surrenders
		/// </summary>
		/// <param name="data">Player authentification identifier</param>
		/// <returns>Response message</returns>
		[HttpPost("leave")]
		public ActionResult Leave(PlayerData data) 
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
			{
				game.DeletePlayer(data.id);
				return new JsonResult(new { success = true });
			}

			// TODO: add more checks when game is in progress

			game.EliminatePlayer(data.id);
			return new JsonResult(new { success = true, message = "You were in an in-progress game, but left anyway" });
		}

		/// <summary>
		/// POST api/game/start
		/// Player attempts to start the game
		/// </summary>
		/// <param name="data">Game rule information</param>
		/// <returns>Response message</returns>
		[HttpPost("start")]
		public ActionResult Start(StartData data) 
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.WaitingForPlayers)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game already started"
				});
			}
			else if (game.GetActivePlayerCount() < 2)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game needs at least 2 players"
				});
			}

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You are not in the game"
				});
			}

			// that's probably enough checks

			game.StartGame(data.finiteDeck, data.onlyNumbers);
			return new JsonResult(new { success = true });
		}

		#endregion

		#region GAMEPLAY

		/// <summary>
		/// POST api/game/play
		/// Player plays a card
		/// </summary>
		/// <param name="data">Player and card information</param>
		/// <returns>Response message</returns>
		[HttpPost("play")]
		public ActionResult Play(PlayData data) 
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You are not in the game"
				});
			}
			else if (game.players[game.activePlayerIndex].id != data.id)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Not your turn"
				});
			}
			else if (game.expectedAction != ExpectedPlayerAction.PlayCard)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't play a card" // maybe have another way of checking
				});
			}

			var card = new Card(data.color, data.type);
			if (!player.hand.Contains(card))
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't play that card"
				});
			}
			else if (!game.CanCardBePlayed(card))
			{
				return new JsonResult(new
				{
					success = false,
					message = "Card cannot be placed on active card"
				});
			}

			// TODO: check if player has to say uno

			if (card.type == CardType.Wild || card.type == CardType.Draw4)// TODO: temp fix for #1
			{
				card.color = CardColor.Red;
			}

			game.PlayerPlaysCard(card);
			return new JsonResult(new { success = true });
		}

		/// <summary>
		/// POST api/game/draw
		/// Player draws a card
		/// </summary>
		/// <param name="data">Player authentification identifier</param>
		/// <returns>Response message</returns>
		[HttpPost("draw")]
		public ActionResult Draw(PlayerData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You are not in the game"
				});
			}
			else if (game.players[game.activePlayerIndex] != player)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Not your turn"
				});
			}
			else if (game.expectedAction != ExpectedPlayerAction.DrawCard)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't draw a card"
				});
			}

            drawCard.Execute();
			return new JsonResult(new { success = true });
		}
    
        // POST api/game/draw/undo
        [HttpPost("draw/undo")]
        public ActionResult UndoDraw() // player draws a card
        {
          drawCard.Undo();
          return new JsonResult(new { success = true });
        }

        /// <summary>
		/// POST api/game/uno
		/// Player says "UNO!" announcing that he has only one card and avoids the penalty of drawing two extra cards
		/// </summary>
		/// <param name="data">Player authentification identifier</param>
		/// <returns>Response message</returns>
		[HttpPost("uno")]
		public ActionResult Uno(PlayerData data) // 
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You are not in the game"
				});
			}

            /*
			else if (game.expectedAction != ExpectedPlayerAction.SayUNO)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You failed to say \"UNO!\""
				});
			}
			//*/

            uno.Execute();
			return new JsonResult(new { success = true });
		}

        // POST api/game/uno/undo
        [HttpPost("uno/undo")]
        public ActionResult UndoUno() // player says "UNO!" announcing that he has only one card and avoids the penalty of drawing two extra cards
        {
            uno.Undo();
            return new JsonResult(new { success = true });
        }

        #endregion

    }
}    