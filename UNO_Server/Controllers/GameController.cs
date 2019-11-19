using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
using UNO_Server.Models.SendData;
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

			return new JsonResult(new { success = true, gamestate = new GameSpectatorState(game) });
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

			return new JsonResult(new { success = true, gamestate = new GamePlayerState(game, player) });
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
			else
			{
				game.EliminatePlayer(data.id);
				return new JsonResult(new { success = true, message = "You were in an in-progress game, but left anyway" });
			}
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
			} // TODO: enable this for live gameplay
			/*
			else if (game.GetActivePlayerCount() < 2)
			{
				return new JsonResult(new
				{
					success = false,
					message = "Game needs at least 2 players"
				});
			}//*/

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

			game.StartGame(data.finiteDeck, data.onlyNumbers, data.slowGame);
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

			var card = new Card(data.color, data.type);
			if (!player.hand.Contains(card))
			{
				return new JsonResult(new
				{
					success = false,
					message = "You don't have that card"
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

			if ((card.type == CardType.Wild || card.type == CardType.Draw4) && card.color == CardColor.Black)
			{
				return new JsonResult(new
				{
					success = false,
					message = "You have to choose a color"
				});
			}

			// TODO: check if player has to say uno

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
			else if (game.CanPlayerPlayAnyOn(player))
			{
				return new JsonResult(new
				{
					success = false,
					message = "You can't draw a card right now"
				});
			}

			drawCard.Execute();
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
			/*
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
			//*/

			uno.Execute();
			return new JsonResult(new { success = true });
		}

		#endregion

		#region TESTING
		[HttpPost("scenario/{scenario}")]
		public ActionResult Scenario(int scenario)
		{
			var game = Game.GetInstance();
			switch (scenario)
			{
				case 0:
					Game.ResetGame();
					return new JsonResult(new { success = true });

				case 1: // Scenario 1: Generic two player game with few but diverse cards
					if (game.GetActivePlayerCount() != 2)
						return new JsonResult(new { success = false, message = "Exactly 2 players must be present" });

					game.phase = GamePhase.Playing;
					game.finiteDeck = false;
					game.flowClockWise = true;

					game.discardPile = new Deck();
					game.discardPile.AddToBottom(new Card(CardColor.Yellow, CardType.Zero));
					game.drawPile = new Utility.BuilderFacade.DeckBuilderFacade()
						.number.SetIndividualNumberCards(0, 1)
						.action.SetSkipCards(1)
						.number.SetIndividualNumberCards(1, 1)
						.wild.SetWildCards(1)
						.action.SetReverseCards(1)
						.action.SetDraw2Cards(1)
						.wild.SetDraw4Cards(1)
						.number.SetIndividualNumberCards(11, 1) // just for tests
						.number.SetIndividualNumberCards(-1, 1) // just for tests
					.Build();

					var p1Hand = new List<Card>();
					p1Hand.Add(new Card(CardColor.Red, CardType.Zero));
					p1Hand.Add(new Card(CardColor.Red, CardType.Zero));
					p1Hand.Add(new Card(CardColor.Red, CardType.One));
					p1Hand.Add(new Card(CardColor.Yellow, CardType.Zero));
					p1Hand.Add(new Card(CardColor.Red, CardType.Skip));
					p1Hand.Add(new Card(CardColor.Red, CardType.Reverse));
					p1Hand.Add(new Card(CardColor.Red, CardType.Draw2));
					var p2Hand = new List<Card>();
					p2Hand.Add(new Card(CardColor.Yellow, CardType.Zero));
					p2Hand.Add(new Card(CardColor.Yellow, CardType.One));
					p2Hand.Add(new Card(CardColor.Yellow, CardType.Skip));
					p2Hand.Add(new Card(CardColor.Yellow, CardType.Reverse));
					p2Hand.Add(new Card(CardColor.Yellow, CardType.Draw2));

					game.players[0].hand = p1Hand;
					game.players[1].hand = p2Hand;
					game.players[1].hand = p2Hand;

					return new JsonResult(new { success = true });

                case 3:
                    game.GameOver();
                    return new JsonResult(new { success = true });

                default:
					return new JsonResult(new { success = false, message = "No such scenario" });
			}

		}
		#endregion

		#region QUESTIONABLE

		// POST api/game/draw/undo
		[HttpPost("draw/undo")]
		public ActionResult UndoDraw()
		{
			drawCard.Undo();
			return new JsonResult(new { success = true });
		}

		// POST api/game/uno/undo
		[HttpPost("uno/undo")]
		public ActionResult UndoUno()
		{
			uno.Undo();
			return new JsonResult(new { success = true });
		}

		#endregion

	}
}