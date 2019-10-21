using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Other
{
	class HttpAdapter
	{
		private const string BASE_URL = "https://localhost:44331/api/game"; //TODO: change this
		private static readonly HttpClient client = new HttpClient();
		
		public async Task<string> SendGetAsync(string playerId)
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

		public async Task<HttpResponseMessage> SendAdvancedPostAsync(string playerId, Models.Card card, string path)
		{
			string JsonString = "{\"id\":\"" + playerId + "\", \"color\":" + card.Color + ",\"type\":" + card.Type + "}";
			var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
			return await client.PostAsync(BASE_URL + path, content);
		}
	}
}
