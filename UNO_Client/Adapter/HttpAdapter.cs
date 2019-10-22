using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Models;

namespace UNO_Client.Adapter
{
	class HttpAdapter : ConnectionInterface
	{
		private readonly string BASE_URL;
		private static readonly HttpClient client = new HttpClient();

		public HttpAdapter(string base_url)
		{
			this.BASE_URL = base_url;
		}
		
		public async Task<string> GetPlayerGameState(string playerId)
		{
			return await client.GetStringAsync(BASE_URL + "/" + playerId);
		}

		public async Task<HttpResponseMessage> SendEmptyPostAsync(string path)
		{
			string JsonString = "{}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + path, content);
		}

		public async Task<HttpResponseMessage> SendSimplePostAsync(string playerId, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\"}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + path, content);
		}


		public async Task<HttpResponseMessage> SendJoinGame(string iden)
		{
			throw new System.NotImplementedException();
		}

		public async Task<HttpResponseMessage> SendLeaveGame(string iden)
		{
			throw new System.NotImplementedException();
		}


		public async Task<HttpResponseMessage> SendDrawCard(string iden)
		{
			throw new System.NotImplementedException();
		}
		public async Task<HttpResponseMessage> SendPlayCard(string iden, Card card)
		{
			string JsonString = "{\"id\":\"" + iden + "\", \"color\":" + card.Color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + "/play", content);
		}

		public async Task<HttpResponseMessage> SendSayUNO(string iden)
		{
			throw new System.NotImplementedException();
		}


		public async Task<HttpResponseMessage> SendUndoDraw()
		{
			throw new System.NotImplementedException();
		}

		public async Task<HttpResponseMessage> SendUndoUNO()
		{
			throw new System.NotImplementedException();
		}
	}
}
