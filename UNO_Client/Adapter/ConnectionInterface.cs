using System.Net.Http;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	interface ConnectionInterface
	{
		Task<string> GetPlayerGameState();

		//Task<HttpResponseMessage> SendJoinGame(); // different parameters?
		Task<HttpResponseMessage> SendLeaveGame();

		Task<HttpResponseMessage> SendDrawCard();
		Task<HttpResponseMessage> SendPlayCard(Card card);
		Task<HttpResponseMessage> SendSayUNO();

		Task<HttpResponseMessage> SendUndoDraw();
		Task<HttpResponseMessage> SendUndoUNO();
	}
}
