using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UNO.Models;

namespace UNO_Server.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
		// GET api/game
		[HttpGet]
		public ActionResult Get()
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

		// GET api/game/5
		[HttpGet("{id}")]
		public ActionResult Get(int id)
		{
			return new JsonResult(new { success = true, id = id });
		}
	}
}