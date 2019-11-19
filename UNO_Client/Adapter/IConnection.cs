using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	interface IConnection
	{
		Task<GameState> GetPlayerGameStateAsync();

		//Task<HttpResponseMessage> SendJoinGame(); // different parameters?
		Task<SimpleResponse> SendLeaveGameAsync();

		Task<SimpleResponse> SendDrawCardAsync();
		Task<SimpleResponse> SendSayUNOAsync();
		Task<SimpleResponse> SendPlayCardAsync(Card card, int color);

		void SendUndoDraw();
		void SendUndoUNO();
	}
}
