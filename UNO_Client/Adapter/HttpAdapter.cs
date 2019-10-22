using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	class HttpAdapter : ConnectionInterface
	{
		private static readonly HttpClient client = new HttpClient();
		private readonly string BASE_URL;
		public string playerId;

		public HttpAdapter(string base_url, string id)
		{
			this.BASE_URL = base_url;
			this.playerId = id;
		}
		
		public async Task<string> GetPlayerGameState()
		{
			return await client.GetStringAsync(BASE_URL + "/" + playerId);
		}


		private async Task<HttpResponseMessage> SimplePostAsync(string playerId, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\"}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + path, content);
		}


		//public async Task<HttpResponseMessage> SendJoinGame()
		//{
		//	throw new System.NotImplementedException();
		//}

		public async Task<HttpResponseMessage> SendLeaveGame()
		{
			return await SimplePostAsync(playerId, "/leave");
		}


		public async Task<HttpResponseMessage> SendDrawCard()
		{
			return await SimplePostAsync(playerId, "/draw");
		}
		public async Task<HttpResponseMessage> SendPlayCard(Card card)
		{
			string JsonString = "{\"id\":\"" + playerId + "\", \"color\":" + card.Color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + "/play", content);
		}

		public async Task<HttpResponseMessage> SendSayUNO()
		{
			return await SimplePostAsync(playerId, "/uno");
		}


		public async Task<HttpResponseMessage> SendUndoDraw()
		{
			var content = new StringContent("{}", Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + "/draw/undo", content);
		}

		public async Task<HttpResponseMessage> SendUndoUNO()
		{
			var content = new StringContent("{}", Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + "/uno/undo", content);
		}
	}
}
