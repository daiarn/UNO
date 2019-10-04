using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO.Models;

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
			return new JsonResult(new
			{
				success = true,
				gamestate = new
				{
					discardPile = new
					{
						count = 2,
						activeCard = new { color = Color.Blue, value = "5" }
					},
					drawPile = new { count = 15 },
					activePlayer = 1,
					players = new[]
					{
						new { name = "alpha", count = 4, isPlaying = true },
						new { name = "bravo", count = 5, isPlaying = true },
						new { name = "charlie", count = 2, isPlaying = true },
						new { name = "delta", count = 4, isPlaying = true }
					}
				}
			});
		}

		// GET api/game/5
		[HttpGet("{id}")]
		public ActionResult Get(int id) // get gamestate of player
		{
			return new JsonResult(new
			{
				success = true,
				gamestate = new
				{
					discardPile = new
					{
						count = 2,
						activeCard = new { color = Color.Blue, value = "5" }
					},
					drawPile = new { count = 15 },
					activePlayer = 1,
					players = new[]
					{
						new { name = "alpha", count = 4, isPlaying = true },
						new { name = "bravo", count = 5, isPlaying = true },
						new { name = "charlie", count = 2, isPlaying = true },
						new { name = "delta", count = 4, isPlaying = true }
					},
					hand = new[]
					{
						new { color = Color.Blue, value = "5" },
						new { color = Color.Green, value = "skip" }
					}
				}
			});
		}

		// POST api/game/leave
		[HttpPost("/leave")]
		public ActionResult Leave() // player leaves/surrenders
		{
			int playerId = 2; // fake id for testing purposes for now

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				game.DeletePlayer(playerId);
				return new JsonResult(new { success = true });
			}

			// game is playing, be more careful

			game.EliminatePlayer(playerId);
			return new JsonResult(new { success = true, message = "You were in a game, but left anyway" });
		}

		#endregion

		#region PRE-GAME PHASE

		// POST api/game/join
		[HttpPost("/join")]
		public ActionResult Join() // new player joins the game
		{
			var name = "Your name here"; // fake name for testing purposes for now 

			var game = Game.GetInstance();

			if (game.phase != GamePhase.WaitingForPlayers)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game already started"
				});
			}
			else if (game.GetActivePlayers() >= 10)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game player capacity exceeded"
				});
			}

			game.AddPlayer(name);
			return new JsonResult(new { success = true });
		}

		// POST api/game/start
		[HttpPost("/start")]
		public ActionResult Start() // a player decides to start the game
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
			else if (game.GetActivePlayers() < 2)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game needs at least 2 players"
				});
			}

			// that's probably enough checks

			game.StartGame();
			return new JsonResult(new { success = true });
		}

		#endregion

		#region GAMEPLAY PHASE

		// POST api/game/play
		[HttpPost("/play")]
		public ActionResult Play() // player plays a card
		{
			int playerId = 5; // fake id for testing purposes for now
			var card = new NumberCard(Color.Blue, 4); // fake card for testing purposes for now

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			Player player = game.GetPlayerById(playerId);

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Player doesn't exist or quit"
				});
			}
			else if (game.activePlayer != playerId)
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


			//game.PlayCard(card);
			return new JsonResult(new { success = true });
		}

		// POST api/game/draw
		[HttpPost("/draw")]
		public ActionResult Draw() // player draws a card
		{
			int playerId = 5; // fake id for testing purposes for now

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			Player player = game.GetPlayerById(playerId);

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Player doesn't exist or quit"
				});
			}
			else if (game.activePlayer != playerId)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Not your turn"
				});
			}

			else if (!player.CanPlayAnyOn(game.discardPile.PeekBottomCard()))
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't play a card you don't have in your hand, you cheater"
				});
			}

			//game.DrawCard();
			return new JsonResult(new { success = true });
		}

		// POST api/game/uno
		[HttpPost("/uno")]
		public ActionResult Uno() // player says "UNO!" announcing that he has only one card and avoids the penalty of drawing two extra cards
		{
			int playerId = 5; // fake id for testing purposes for now
			//var card = new NumberCard(Color.Blue, 4); // fake card for testing purposes for now

			var game = Game.GetInstance();

			if (game.phase != GamePhase.Playing)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game isn't started"
				});
			}

			Player player = game.GetPlayerById(playerId);

			if (player == null)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Player doesn't exist or quit"
				});
			}
			/*
			else if (game.activePlayer != playerId) // TODO: figure this out
			{
				return new JsonResult(new
				{
					success = false,
					message = "Not your turn"
				});
			}
			//*/


			//game.sayUNO();
			return new JsonResult(new { success = true });
		}

		#endregion


	}
}