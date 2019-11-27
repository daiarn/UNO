using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO_Server.Models;
using UNO_Server.Models.RecvData;
using UNO_Server.Models.SendData;
using UNO_Server.Models.SendResult;
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

		[HttpGet]
		public ActionResult<BaseResult> Get()
		{
			var game = Game.GetInstance();

			return new GamestateResult(new GameSpectatorState(game));
		}

		[HttpGet("{id}")]
		public ActionResult<BaseResult> Get(Guid id)
		{
			var game = Game.GetInstance();
			var player = game.GetPlayerByUUID(id);

			if (player == null)
				return new FailResult("You are not in the game");

			return new GamestateResult(new GamePlayerState(game, player));
		}

		#endregion



		#region NON-GAMEPLAY

		[HttpPost("join")]
		public ActionResult<BaseResult> Join(JoinData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.WaitingForPlayers)
				return new FailResult("Game already started");
			else if (game.GetActivePlayerCount() >= 10)
				return new FailResult("Game player capacity exceeded");

			return new JoinResult(game.AddPlayer(data.name));
		}

		[HttpPost("leave")]
		public ActionResult<BaseResult> Leave(PlayerData data)
		{
			var game = Game.GetInstance();
			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
				return new FailResult("You are not in the game");

			if (game.phase != GamePhase.Playing)
				game.DeletePlayer(data.id);
			else
				game.EliminatePlayer(data.id);

			return new BaseResult();
		}

		[HttpPost("start")]
		public ActionResult<BaseResult> Start(StartData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.WaitingForPlayers)
				return new FailResult("Game already started");
			//else if (game.GetActivePlayerCount() < 2)// TODO: enable this for live gameplay
			  //return new FailResult("Game needs at least 2 players");

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
				return new FailResult("You are not in the game");

			// that's probably enough checks

			game.StartGame(data.finiteDeck, data.onlyNumbers, data.slowGame);
			return new BaseResult();
		}

		#endregion

		#region GAMEPLAY

		[HttpPost("play")]
		public ActionResult<BaseResult> Play(PlayData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
				return new FailResult("Game isn't started");

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
				return new FailResult("You are not in the game");
			else if (game.players[game.activePlayerIndex].id != data.id)
				return new FailResult("Not your turn");

			var card = new Card(data.color, data.type);
			if (!player.hand.Contains(card))
				return new FailResult("You don't have that card");
			else if (!game.CanCardBePlayed(card))
				return new FailResult("Card cannot be placed on active card");

			if ((card.type == CardType.Wild || card.type == CardType.Draw4) && card.color == CardColor.Black)
				return new FailResult("You have to choose a color");

			// TODO: check if player has to say uno

			game.PlayerPlaysCard(card);
			return new BaseResult();
		}

		[HttpPost("draw")]
		public ActionResult<BaseResult> Draw(PlayerData data)
		{
			var game = Game.GetInstance();
			if (game.phase != GamePhase.Playing)
				return new FailResult("Game isn't started");

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
				return new FailResult("You are not in the game");
			else if (game.players[game.activePlayerIndex] != player)
				return new FailResult("Not your turn");
			else if (game.CanPlayerPlayAnyOn(player))
				return new FailResult("You can't draw a card right now");

			game.PlayerDrawsCard();
			//drawCard.Execute();

			return new BaseResult();
		}

		[HttpPost("uno")]
		public ActionResult<BaseResult> Uno(PlayerData data) // 
		{
			var game = Game.GetInstance();
			/*
			if (game.phase != GamePhase.Playing)
				return new FailResult("Game isn't started");

			var player = game.GetPlayerByUUID(data.id);
			if (player == null)
				return new FailResult("You are not in the game");
			//*/

			game.PlayerSaysUNO();
			//uno.Execute();
			return new BaseResult();
		}

		#endregion

		#region TESTING
		[HttpPost("scenario/{scenario}")]
		public ActionResult<BaseResult> Scenario(int scenario)
		{
			var game = Game.GetInstance();
			switch (scenario)
			{
				case 0:
					Game.ResetGame();
					return new BaseResult();

				case 1: // Scenario 1: Generic two player game with few but diverse cards
					if (game.GetActivePlayerCount() != 2)
						return new FailResult("Exactly 2 players must be present");

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

					return new BaseResult();

				case 3:
					game.GameOver();
					return new BaseResult();

				default:
					return new FailResult("No such scenario");
			}

		}
		#endregion

		[HttpPost("draw/undo")]
		public ActionResult<BaseResult> UndoDraw()
		{
			drawCard.Undo();
			return new BaseResult();
		}

		[HttpPost("uno/undo")]
		public ActionResult<BaseResult> UndoUno()
		{
			uno.Undo();
			return new BaseResult();
		}
	}
}