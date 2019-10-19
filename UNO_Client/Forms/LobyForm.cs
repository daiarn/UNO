using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNO_Client.Models;

namespace UNO_Client.Forms
{
    public partial class LobyForm : Form
    {
        private const string BASE_URL = "https://localhost:44331/api/game"; //TODO: change this
        private static readonly HttpClient client = new HttpClient();
        private JoinPost joinPost;
        private Game Game;
        public LobyForm(JoinPost joinPost)
        {
            this.joinPost = joinPost;
            SetGame();
        }

        private void LobyForm_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool isOnlyNumbers = gameOptions.GetItemChecked(0);
            bool isFiniteDeck = gameOptions.GetItemChecked(1);
            //Start game
            string JsonString = "{\"finiteDeck\":\"" + isFiniteDeck + "\",\"onlyNumbers\":\"" + isOnlyNumbers +"\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/start", content);
            GameForm form = new GameForm(joinPost);
            this.Close();
            form.Show();
        }

        private void gameOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void SetGame()
        {
            var respondeString = await client.GetStringAsync(BASE_URL + "/" + joinPost.Id);
            //json serializer to Game object and set it globaly
            Game = JsonConvert.DeserializeObject<Game>(respondeString);
            InitializeComponent();
            ShowPlayersInformation();
        }
        private string[] FormatPlayersInformation()
        {
            int count = Game.Gamestate.Players.Count;
            string[] infomration = new string[count];
            for (int i = 0; i < count; i++)
            {
                var player = Game.Gamestate.Players[i];
                infomration[i] = player.Name;
            }
            return infomration;
        }
        private void ShowPlayersInformation()
        {
            var info = FormatPlayersInformation();
            Players.Lines = info;
        }
    }
}
