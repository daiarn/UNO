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

		public HttpAdapter(string base_url, string id)
		{
			this.BASE_URL = base_url;
			this.playerId = id;
		}

		public async Task<GameState> GetPlayerGameStateAsync()
		{
			var respondString = await client.GetStringAsync(BASE_URL + "/" + playerId);
			var game = JsonConvert.DeserializeObject<Game>(respondString);
			return game.Gamestate;
		}

		private Task<HttpResponseMessage> SimplePostAsync(string playerId, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\"}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return client.PostAsync(BASE_URL + path, content);
		}

		public async Task<SimpleResponse> SendLeaveGameAsync()
		{
			var request = await SimplePostAsync(playerId, "/leave");
			var response = JsonConvert.DeserializeObject<SimpleResponse>(await request.Content.ReadAsStringAsync());
			return response;
		}

		public async Task<SimpleResponse> SendDrawCardAsync()
		{
			var request = await SimplePostAsync(playerId, "/draw");
			var response = JsonConvert.DeserializeObject<SimpleResponse>(await request.Content.ReadAsStringAsync());
			return response;
		}

		public async Task<SimpleResponse> SendSayUNOAsync()
		{
			var request = await SimplePostAsync(playerId, "/uno");
			var response = JsonConvert.DeserializeObject<SimpleResponse>(await request.Content.ReadAsStringAsync());
			return response;
		}

		public async Task<SimpleResponse> SendPlayCardAsync(Card card, int color)
		{
			string JsonString = "{\"id\":\"" + playerId + "\", \"color\":" + color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			var request = await client.PostAsync(BASE_URL + "/play", content);
			var response = JsonConvert.DeserializeObject<SimpleResponse>(await request.Content.ReadAsStringAsync());
			return response;
		}

		public void SendUndoDraw()
		{
			//var content = new StringContent("{}", Encoding.UTF8, "application/json");
			//client.PostAsync(BASE_URL + "/draw/undo", content);
		}

		public void SendUndoUNO()
		{
			//var content = new StringContent("{}", Encoding.UTF8, "application/json");
			//client.PostAsync(BASE_URL + "/uno/undo", content);
		}
	}
}
