using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	interface ConnectionInterface
	{
		Task<string> GetPlayerGameState();

		//Task<HttpResponseMessage> SendJoinGame(); // different parameters?
		bool SendLeaveGame();

		bool SendDrawCard();
		bool SendPlayCard(Card card);
		bool SendSayUNO();

		void SendUndoDraw();
		void SendUndoUNO();
	}
}
