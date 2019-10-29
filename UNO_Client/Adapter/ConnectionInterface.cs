using System.Net.Http;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	interface ConnectionInterface // TODO: refactor return types
	{
		Task<string> GetPlayerGameState();

		//Task<HttpResponseMessage> SendJoinGame(); // different parameters?
		void SendLeaveGame();

		void SendDrawCard();
		void SendPlayCard(Card card);
		void SendSayUNO();

		void SendUndoDraw();
		void SendUndoUNO();
	}
}
