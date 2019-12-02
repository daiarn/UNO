using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	class HttpAdapter : IConnection
	{
		private static readonly HttpClient client = new HttpClient();
		private readonly string BASE_URL;
		public string playerId;

		public HttpAdapter(string base_url)
		{
			BASE_URL = base_url;
		}
		public void SetIdentifier(string id)
		{
			playerId = id;
		}

		private string OnlyPlayerId() => "{\"id\":\"" + playerId + "\"}";

		private async Task<T> PostJsonAsync<T>(string jsonString, string endpoint) // TODO: change from string to object
		{
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
			var request = await client.PostAsync(BASE_URL + endpoint, content);
			var response = JsonConvert.DeserializeObject<T>(await request.Content.ReadAsStringAsync());
			return response;
		}

		// Universal

		public async Task<GameState> GetPlayerGameStateAsync()
		{
			var respondString = await client.GetStringAsync(BASE_URL + "/" + playerId);
			var game = JsonConvert.DeserializeObject<Game>(respondString);
			return game.Gamestate;
		}

		// Non-gameplay

		public async Task<JoinResponse> SendJoinGame(string name)
		{
			return await PostJsonAsync<JoinResponse>("{\"name\":\"" + name + "\"}", "/join");
		}

		public async Task<SimpleResponse> SendStartGame(bool finiteDeck, bool onlyNumbers, bool slowGame)
		{
			return await PostJsonAsync<SimpleResponse>("{\"id\":\"" + playerId + "\",\"finiteDeck\":\"" + finiteDeck + "\",\"onlyNumbers\":\"" + onlyNumbers + "\",\"slowGame\":\"" + slowGame + "\"}", "/start");
		}

		// Gameplay

		public Task<SimpleResponse> SendLeaveGameAsync()
		{
			return PostJsonAsync<SimpleResponse>(OnlyPlayerId(), "/leave");
		}

		public Task<SimpleResponse> SendDrawCardAsync()
		{
			return PostJsonAsync<SimpleResponse>(OnlyPlayerId(), "/draw");
		}

		public Task<SimpleResponse> SendSayUnoAsync()
		{
			return PostJsonAsync<SimpleResponse>(OnlyPlayerId(), "/uno");
		}

		public async Task<SimpleResponse> SendPlayCardAsync(Card card, int color)
		{
			string jsonString = "{\"id\":\"" + playerId + "\", \"color\":" + color + ",\"type\":" + card.Type + "}";
			return await PostJsonAsync<SimpleResponse>(jsonString, "/play");
		}

		public Task<SimpleResponse> SendUndoAsync()
		{
			return PostJsonAsync<SimpleResponse>("", "/undo");
		}
	}
}
