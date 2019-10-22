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
		//public string identity;

		public HttpAdapter(string base_url)//, string id)
		{
			this.BASE_URL = base_url;
			//this.identity = id;
		}
		
		public async Task<string> GetPlayerGameState(string playerId)
		{
			return await client.GetStringAsync(BASE_URL + "/" + playerId);
		}


		private async Task<HttpResponseMessage> SimplePostAsync(string playerId, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\"}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + path, content);
		}


		//public async Task<HttpResponseMessage> SendJoinGame(string iden)
		//{
		//	throw new System.NotImplementedException();
		//}

		public async Task<HttpResponseMessage> SendLeaveGame(string iden)
		{
			return await SimplePostAsync(iden, "/leave");
		}


		public async Task<HttpResponseMessage> SendDrawCard(string iden)
		{
			return await SimplePostAsync(iden, "/draw");
		}
		public async Task<HttpResponseMessage> SendPlayCard(string iden, Card card)
		{
			string JsonString = "{\"id\":\"" + iden + "\", \"color\":" + card.Color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + "/play", content);
		}

		public async Task<HttpResponseMessage> SendSayUNO(string iden)
		{
			return await SimplePostAsync(iden, "/uno");
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
