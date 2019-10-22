using System.Net.Http;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	interface ConnectionInterface
	{
		Task<string> GetPlayerGameState(string iden);

		Task<HttpResponseMessage> SendJoinGame(string iden);
		Task<HttpResponseMessage> SendLeaveGame(string iden);

		Task<HttpResponseMessage> SendDrawCard(string iden);
		Task<HttpResponseMessage> SendPlayCard(string iden, Card card);
		Task<HttpResponseMessage> SendSayUNO(string iden);

		Task<HttpResponseMessage> SendUndoDraw();
		Task<HttpResponseMessage> SendUndoUNO();
	}
}
