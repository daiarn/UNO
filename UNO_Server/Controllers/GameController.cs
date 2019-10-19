using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO_Server.Models;

namespace UNO_Server.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
	{
		#region UNIVERSAL

		// GET api/game
		[HttpGet]
		public ActionResult Get() // for spectators maybe
		{
			var game = Game.GetInstance();

			List<object> allPlayerData = new List<object>();
			foreach (var player in game.players)
			{
				if (player == null) break;
				allPlayerData.Add(new { player.name, count = player.hand.Count(), player.isPlaying });
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

		// GET api/game/5
		[HttpGet("{id}")]
		public ActionResult Get(Guid id) // get gamestate of player
		{
			var game = Game.GetInstance();

			List<object> allPlayerData = new List<object>();
			foreach (var item in game.players)
			{
				if (item == null) break;
				allPlayerData.Add(new { name = item.name, count = item.hand.Count(), isPlaying = item.isPlaying });
			}

			Player player = game.GetPlayerByUUID(id);

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

		// POST api/game/leave
		[HttpPost("leave")]
		public ActionResult Leave(PlayerData data) // player leaves/surrenders
		{
			Guid playerId = data.id;

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				game.DeletePlayer(playerId);
				return new JsonResult(new { success = true });
			}

			// TODO: more checks when game is playing

			game.EliminatePlayer(playerId);
			return new JsonResult(new { success = true, message = "You were in an in-progress game, but left anyway" });
		}

		#endregion

		#region PRE-GAME PHASE

		// POST api/game/join
		[HttpPost("join")]
		public ActionResult Join(JoinData dta) // new player joins the game
		{
			var name = dta.name;
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

			Guid id = game.AddPlayer(name);
			return new JsonResult(new { success = true, id = id });
		}

		// POST api/game/start
		[HttpPost("start")]
		public ActionResult Start(StartData data) // a player decides to start the game
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

			// that's probably enough checks

			game.StartGame(data.finiteDeck, data.onlyNumbers);
			return new JsonResult(new { success = true });
		}

		#endregion

		#region GAMEPLAY PHASE

		// POST api/game/play
		[HttpPost("play")]
		public ActionResult Play(PlayData data) // player plays a card
		{
			Guid playerId = data.id;
			var card = new Card(data.color, data.type);

			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			Player player = game.GetPlayerByUUID(playerId);

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You don't exist or quit"
				});
			}
			else if (game.players[game.activePlayerIndex].id != playerId)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Not your turn"
				});
			}
			else if (!player.hand.Contains(card))
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't play a card you don't have in your hand, you cheater"
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

			// looks good, go ahead


			game.PlayerPlaysCard(player, card);
			return new JsonResult(new { success = true });
		}

		// POST api/game/draw
		[HttpPost("draw")]
		public ActionResult Draw(PlayerData data) // player draws a card
		{
			var game = Game.GetInstance();
			Player player = game.GetPlayerByUUID(data.id);

			// TODO: uncomment checks after testing
			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Player doesn't exist or quit"
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

			// TODO: check if player has playable card

			game.PlayerDrawsCard(player);
			return new JsonResult(new { success = true });
		}

		// POST api/game/uno
		[HttpPost("uno")]
		public ActionResult Uno(PlayerData data) // player says "UNO!" announcing that he has only one card and avoids the penalty of drawing two extra cards
		{
			Guid playerId = data.id;

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			Player player = game.GetPlayerByUUID(playerId);

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Player doesn't exist or quit"
				});
			}

			/*
			else if (game.activePlayer != playerId)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You failed to say \"UNO!\""
				});
			}
			//*/


			//game.sayUNO();
			return new JsonResult(new { success = true });
		}

		#endregion


	}
}