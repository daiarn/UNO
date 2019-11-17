using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using UNO_Client.Models;

namespace UNO_Client.Forms
{
    public partial class LobyForm : Form
    {
        private const string BASE_URL = "https://localhost:44331/api/game"; //TODO: change this
        private static readonly HttpClient client = new HttpClient();
        private static System.Windows.Forms.Timer GameTimer;
        private JoinPost joinPost;
        private Game Game;
        public LobyForm(JoinPost joinPost)
        {
            this.joinPost = joinPost;
            SetGame();
            Thread.Sleep(1000);
            InitializeComponent();
            SetGameTimer();
        }

        private void LobyForm_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool isOnlyNumbers = gameOptions.GetItemChecked(0);
            bool isFiniteDeck = gameOptions.GetItemChecked(1);
            //Start game
            string JsonString = "{\"id\":\"" + joinPost.Id + "\",\"finiteDeck\":\"" + isFiniteDeck + "\",\"onlyNumbers\":\"" + isOnlyNumbers +"\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/start", content);
            GameTimer.Stop();
            GameTimer.Dispose();
            OpenGameForm();
        }
        private void OpenGameForm()
        {
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
            if (IsGameStarted())
            {
                OpenGameForm();
            }
            else
            {
                ShowPlayersInformation();
            }
        }
        private bool IsGameStarted()
        {
            if (Game.Gamestate.GamePhase == GamePhase.Playing)
            {
                GameTimer.Stop();
                GameTimer.Dispose();
                return true;
            }
            return false;
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
        private void SetGameTimer()
        {
            // Create a timer with a two second interval.
            GameTimer = new System.Windows.Forms.Timer();
            GameTimer.Tick += new EventHandler(OnTimedGameEvent);
            // Hook up the Elapsed event for the timer. 
            GameTimer.Interval = 2000;
            GameTimer.Start();
        }
        private void OnTimedGameEvent(Object myObject, EventArgs myEventArgs)
        {
            GameTimer.Stop();
            //Fetch game data
            SetGame();
            GameTimer.Enabled = true;
        }
    }
}
