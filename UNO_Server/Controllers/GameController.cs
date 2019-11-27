using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO_Server.ChainOfResponsibility;
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
			var chain = new CheckIfPlayerExists(id)
				.Then(new ConcludeAndExecute(
					g => new GamestateResult(new GamePlayerState(g, id))
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		#endregion



		#region NON-GAMEPLAY

		[HttpPost("join")]
		public ActionResult<BaseResult> Join(JoinData data)
		{
			var chain = new CheckGamePhase(GamePhase.WaitingForPlayers)
				.Then(new CheckCustomPredicate(g => g.GetActivePlayerCount() < 10, "Game player capacity exceeded"))
				.Then(new ConcludeAndExecute(
					g => new JoinResult(g.AddPlayer(data.name))
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		[HttpPost("leave")]
		public ActionResult<BaseResult> Leave(PlayerData data)
		{
			var chain = new CheckIfPlayerExists(data.id)
				.Then(new ConcludeAndExecute(
					g => { g.EliminatePlayer(data.id); return new BaseResult(); }
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		[HttpPost("start")]
		public ActionResult<BaseResult> Start(StartData data)
		{
			var chain = new CheckGamePhase(GamePhase.WaitingForPlayers)
				//.Then(new CheckCustomPredicate(g => g.GetActivePlayerCount() >= 2, "Game needs at least 2 players")) // TODO: enable this for live gameplay
				.Then(new CheckIfPlayerExists(data.id))
				.Then(new ConcludeAndExecute(
					g => { g.StartGame(data.finiteDeck, data.onlyNumbers, data.slowGame); return new BaseResult(); }
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		#endregion

		#region GAMEPLAY

		[HttpPost("play")]
		public ActionResult<BaseResult> Play(PlayData data)
		{
			var card = new Card(data.color, data.type);
			var chain = new CheckGamePhase(GamePhase.Playing)
				.Then(new CheckIfPlayerExists(data.id))
				.Then(new CheckIfPlayerTurn(data.id))
				.Then(new CheckCustomPredicate(g => g.GetPlayerByUUID(data.id).hand.Contains(card), "You don't have that card"))
				.Then(new CheckCustomPredicate(g => g.CanCardBePlayed(card), "You can't play that card"))
				.Then(new CheckCustomPredicate(
					g => (card.type == CardType.Wild || card.type == CardType.Draw4) && card.color == CardColor.Black, "You have to choose a color"))
				.Then(new ConcludeAndExecute(
					g => { g.PlayerPlaysCard(card); return new BaseResult(); }
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		[HttpPost("draw")]
		public ActionResult<BaseResult> Draw(PlayerData data)
		{
			var chain = new CheckGamePhase(GamePhase.Playing)
				.Then(new CheckIfPlayerExists(data.id))
				.Then(new CheckIfPlayerTurn(data.id))
				.Then(new CheckCustomPredicate(g => g.CanPlayerPlayAnyOn(g.GetPlayerByUUID(data.id)), "You can't draw a card right now"))
				.Then(new ConcludeAndExecute(
					g => { g.PlayerDrawsCard(); return new BaseResult(); }
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
		}

		[HttpPost("uno")]
		public ActionResult<BaseResult> Uno(PlayerData data)
		{
			var chain = new CheckGamePhase(GamePhase.Playing)
				.Then(new CheckIfPlayerExists(data.id))
				.Then(new CheckIfPlayerTurn(data.id))
				// TODO: add a few more here?
				.Then(new ConcludeAndExecute(
					g => { g.PlayerSaysUNO(); return new BaseResult(); }
				));

			var game = Game.GetInstance();
			return chain.ProcessChain(game);
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
						return new FailResult("Exactly 2 players must be active");

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

				case 9:
					game.GameOver();
					return new BaseResult();

				default:
					return new FailResult("No such scenario");
			}

		}
		#endregion

		DrawCard drawCard = new DrawCard();
		Uno uno = new Uno();

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