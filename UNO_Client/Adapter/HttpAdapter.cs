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


		private Task<HttpResponseMessage> SimplePostAsync(string playerId, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\"}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			var response = client.PostAsync(BASE_URL + path, content);

			return response;
		}


		//public async Task<HttpResponseMessage> SendJoinGame()
		//{
		//	throw new System.NotImplementedException();
		//}

		public bool SendLeaveGame()
		{
			var response = SimplePostAsync(playerId, "/leave");
			return true;
		}


		public bool SendDrawCard()
		{
			var response = SimplePostAsync(playerId, "/draw");
			return true;
		}
		public bool SendPlayCard(Card card)
		{
			string JsonString = "{\"id\":\"" + playerId + "\", \"color\":" + card.Color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			var response = client.PostAsync(BASE_URL + "/play", content);
			return true;
		}

		public bool SendSayUNO()
		{
			var response = SimplePostAsync(playerId, "/uno");
			return true;
		}


		public void SendUndoDraw()
		{
			var content = new StringContent("{}", Encoding.UTF8, "application/json");
			client.PostAsync(BASE_URL + "/draw/undo", content);
		}

		public void SendUndoUNO()
		{
			var content = new StringContent("{}", Encoding.UTF8, "application/json");
			client.PostAsync(BASE_URL + "/uno/undo", content);
		}
	}
}
