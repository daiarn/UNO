using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using UNO_Client.Models;
using Newtonsoft.Json;
using System.Timers;

namespace UNO_Client.Forms
{
    public partial class GameForm : Form
    {
        public float[,] xyImage;
        private const string BASE_URL = "https://localhost:44331/api/game"; //TODO: change this

        private static System.Timers.Timer PlayerTimer;
        private static System.Timers.Timer GameTimer;
        private static readonly HttpClient client = new HttpClient();
        private Game Game;
        private Player CurrentPlayer;

        public GameForm()
        {
            CurrentPlayer = new Player();
            CurrentPlayer.Id = "4a737078-e065-489a-a15f-64ab6eae959e"; //Player id for testing
            SetGame();

            
           
            //CurrentPlayer.Cards = new List<Card>
            //{
            //    new Card("b", "0", "b0.png"),
            //    new Card("g", "7", "g7.png"),
            //    new Card("b", "5", "b5.png")
            //};
        }

        public GameForm(JoinPost joinPost)
        {
            CurrentPlayer = new Player();
            CurrentPlayer.Id = joinPost.Id;
            SetGame();
        }

        private async void Draw_ClickAsync(object sender, EventArgs e)
        {
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/draw", content);
        }

        private async void GiveUp_Click(object sender, EventArgs e)
        {
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/leave", content);
        }

        private async void UNO_Click(object sender, EventArgs e)
        {
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/uno", content);
        }

        private async void Exit_Click(object sender, EventArgs e)
        {
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/leave", content);
        }

        const float HandCardWidth = 80f;

        private void HandPanel_Paint(object sender, PaintEventArgs e)
        {
            if(Game != null)
            {
                var widthPerCard = HandCardWidth / 2;
                var handCount = Game.Gamestate.Hand.Count;

                var graphics = e.Graphics;
                float movePosition = 0f;
                xyImage = new float[handCount, 2];

                float width = (float)handPanel.Width;
                float height = (float)handPanel.Height;
                float middlePoint = width / 4f;

                if (handCount * widthPerCard > (width - middlePoint))
                {
                    middlePoint = width - (handCount * widthPerCard);

                    if (middlePoint < 0)
                    {
                        middlePoint = 0;
                        widthPerCard = width / handCount;
                    }
                }

                for (int i = 0; i < handCount; i++)
                {
                    var card = Game.Gamestate.Hand[i];
                    var img = card.GetImage();

                    xyImage[i, 0] = middlePoint + movePosition;
                    xyImage[i, 1] = 15f; //Recommended cards
                    movePosition += widthPerCard;

                    var dbWidth = HandCardWidth / (float)img.Width;
                    var dbHeight = dbWidth * img.Height;

                    graphics.DrawImage(img, new RectangleF(xyImage[i, 0], xyImage[i, 1], HandCardWidth, dbHeight));
                }
            }
        }

        private void handPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int bild = HitTestCard(e.Location);
            if (bild != -1)
            {
                var card = Game.Gamestate.Hand[bild];
                var color = card.Color;

                if (color == "black")
                {
                    var chooser = new ColorChooserForm();
                    chooser.ShowDialog(this);
                    color = chooser.Color;
                }

                putCard(card, color);
            }
        }

        private void handPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //DO NOTHING
        }

        int HitTestCard(Point loc)
        {
            var x = loc.X;
            var y = loc.Y;

            int bild = -1;
            for (int i = 0; i < Game.Gamestate.Hand.Count; i++)
            {
                var img = Game.Gamestate.Hand[i].GetImage();

                var dblFac = HandCardWidth / (float)img.Width;
                var dblHeight = dblFac * img.Height;

                float test2 = xyImage[i, 0] + HandCardWidth;

                if (x >= xyImage[i, 0] && x <= xyImage[i, 0] + HandCardWidth && y >= xyImage[i, 1] && y <= xyImage[i, 1] + dblHeight)
                {
                    bild = i;
                }

            }
            return bild;

        }

        public const double TAU = 2 * Math.PI;
        Font playerFont = SystemFonts.CaptionFont;
        Font activePlayerFont;
        Bitmap blankCardImage = new Bitmap("..//..//CardImages//r0.png"); //TODO: FIX IT FIXED but it is not blank

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            if (activePlayerFont == null)
            {
                activePlayerFont = new Font(playerFont, FontStyle.Bold);
            }

            var graphics = e.Graphics;
            var width = (float)mainPanel.Width;
            var height = (float)mainPanel.Height;
            var middlePointX = width / 2f;
            var middlePointY = height / 2f;

            //var img = Game.TopCard.GetImage();

            float dbWidth = 80f;
            float dbFac = dbWidth / (float)blankCardImage.Width;
            var dbHeight = dbFac * blankCardImage.Height;

            graphics.DrawImage(Game.Gamestate.ActiveCard.GetImage(), new RectangleF(middlePointX - dbWidth / 2f, (middlePointY + dbHeight) < height ? middlePointY : (height - dbHeight), dbWidth, dbHeight));

            //var gama = Math.Acos(1.0 - (Math.Pow(width, 2) / (2.0 * Math.Pow(middlePointY, 2.0) + 0.5 * Math.Pow(width, 2.0))));

            //var numOtherPlayers = Game.Players.Count;
            //var distance = (TAU - gama) / (double)numOtherPlayers;

            //var radius = Math.Min(middlePointX, middlePointY);
            //var phase = -Math.PI / 2 + gama / 2;

            //dbWidth = 60f;
            //dbFac = dbWidth / (float)img.Width;
            //dbHeight = dbFac * img.Height;

            //for (int i = 1; i < numOtherPlayers; i++)
            //{
            //    Player player = Game.Players[i];
            //    if (player.PlayerId == CurrentPlayer.PlayerId)
            //    {
            //        i++;
            //        player = Game.Players[i];
            //    }

            //    var p_X = middlePointX + radius * (float)Math.Cos(distance * i + phase);
            //    var p_Y = middlePointY - radius * (float)Math.Sin(distance * i + phase);

            //    var font = player.PlayerId == CurrentPlayer.PlayerId ? activePlayerFont : playerFont;
            //    var size = graphics.MeasureString(player.Nick, font);
            //    graphics.DrawString(player.Nick, font, Brushes.Black, p_X - size.Width / 2, p_Y);

            //    graphics.DrawImage(blankCardImage, p_X - dbWidth / 2, p_Y += size.Height, dbWidth, dbHeight);

            //    var numString = player.Cards.Count.ToString();

            //    size = graphics.MeasureString(numString, font);

            //    graphics.DrawString(numString, font, Brushes.Black, p_X - size.Width / 2, p_Y + dbHeight / 2 - size.Height / 2);
            //}
        }

        private async void putCard(Card card, string Color)
        {
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\", \"color\":\"" + card.Color + "\",\"type\":\"" + card.Value + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/play", content);
        }

        private async void SetGame()
        {
            var respondeString = await client.GetStringAsync(BASE_URL + "/" + CurrentPlayer.Id);
            //json serializer to Game object and set it globaly
            Game = JsonConvert.DeserializeObject<Game>(respondeString);
            InitializeComponent();
            ShowPlayersInformation();
        }

        private void SetPlayerTimer()
        {
            // Create a timer with a two second interval.
            PlayerTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            PlayerTimer.Elapsed += OnTimedEvent;
            PlayerTimer.AutoReset = true;
            PlayerTimer.Enabled = true;
        }
        private void SetGameTimer()
        {
            // Create a timer with a two second interval.
            GameTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            PlayerTimer.Elapsed += OnTimedGameEvent;
            PlayerTimer.AutoReset = true;
            PlayerTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Fetch user data
            //SetPlayer();
        }

        private void OnTimedGameEvent(Object source, ElapsedEventArgs e)
        {
            //Fetch game data
            SetGame();          
        }

        private async void StartGame_Click(object sender, EventArgs e)
        {
            //Start game
            string JsonString = "{\"id\":\"" + CurrentPlayer.Id + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/start", content);
            StartGame.Visible = false;
        }

        private string[] FormatPlayersInformation()
        {
            int count = Game.Gamestate.Players.Count;
            string[] infomration = new string[count];
            for (int i = 0; i < count; i++)
            {
                var player = Game.Gamestate.Players[i];
                string line = player.Name + " " + player.Count;
                infomration[i] = line;
            }
            return infomration;
        }
        private void ShowPlayersInformation()
        {
            var info = FormatPlayersInformation();
            PlayersInfo.Lines = info;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
