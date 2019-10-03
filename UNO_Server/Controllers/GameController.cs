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
		// returns game state
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new JsonResult(new
			{
				success = true,
				gamestate = new
				{
					discardPile = new
					{
						count = 2,
						activeCard = new { color = Color.Blue, type = "number", value = 5 }
					},
					drawPile = new { count = 15 },
					players = new[]
					{
						new { count = 4 },
						new { count = 5 },
						new { count = 2 },
						new { count = 4 }
					},
					hand = new[]
					{
						new { color = Color.Blue, type = "number", value = 5 },
						new { color = Color.Green, type = "number", value = 4 }
					}
				}
			});
		}
	}
}