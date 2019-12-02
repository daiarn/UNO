using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	public interface IConnection
	{
		void SetIdentifier(string id);
		Task<GameState> GetPlayerGameStateAsync();

		Task<SimpleResponse> SendStartGame(bool finiteDeck, bool onlyNumbers, bool slowGame);
		Task<JoinResponse> SendJoinGame(string name);
		Task<SimpleResponse> SendLeaveGameAsync();

		Task<SimpleResponse> SendDrawCardAsync();
		Task<SimpleResponse> SendSayUnoAsync();
		Task<SimpleResponse> SendPlayCardAsync(Card card, int color);

		Task<SimpleResponse> SendUndoAsync();
	}
}
