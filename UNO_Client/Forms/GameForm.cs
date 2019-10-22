﻿using System;
using System.Drawing;
using System.Windows.Forms;
using UNO_Client.Models;
using Newtonsoft.Json;
using System.Threading;
using UNO_Client.Other;
using UNO_Client.Decorator;
using UNO_Client.Adapter;

namespace UNO_Client.Forms
{
	public partial class GameForm : Form
    {
        public float[,] xyImage;

        private static System.Windows.Forms.Timer GameTimer;
		private static readonly ConnectionInterface serverConnection = new HttpAdapter("https://localhost:44331/api/game"); // TODO: change this
        private static readonly SoundAdapter soundAdaptor = new SoundAdapter();
		private Game Game;
		private string CurrentPlayerId;

		public GameForm(JoinPost joinPost)
        {
            CurrentPlayerId = joinPost.Id;
            SetGame();
            Thread.Sleep(1000);
            InitializeComponent();
            SetGameTimer();
        }

        private async void Draw_ClickAsync(object sender, EventArgs e)
        {
			var response = await serverConnection.SendSimplePostAsync(CurrentPlayerId, "/draw");
            SetGame();
        }

        private async void GiveUp_Click(object sender, EventArgs e)
        {
			var response = await serverConnection.SendSimplePostAsync(CurrentPlayerId, "/leave");
			SetGame();
        }

        private async void UNO_Click(object sender, EventArgs e)
        {
			var response = await serverConnection.SendSimplePostAsync(CurrentPlayerId, "/uno");
            soundAdaptor.turnOnSoundEffect();

			SetGame();
        }

        private async void Exit_Click(object sender, EventArgs e)
        {
			var response = await serverConnection.SendSimplePostAsync(CurrentPlayerId, "/leave");
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
        Bitmap blankCardImage = new Bitmap("..//..//CardImages//r0.png");

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

            RectangleF rect = new RectangleF(middlePointX - (dbWidth / 2), middlePointY - dbHeight - (dbHeight / 4), dbWidth, dbHeight);
            RectangleF rectToDecorate = new RectangleF(middlePointX - (dbWidth / 2) - 5, middlePointY - dbHeight - (dbHeight / 4), dbWidth + 10, dbHeight);
            Rect simpleRect = new DiagonalDecorator(new BorderDecorator(new BackgroundDecorator(new SimpleRect(rectToDecorate, graphics))));
            simpleRect.Draw();
            graphics.DrawImage(Game.Gamestate.ActiveCard.GetImage(), rect);

        }

        private async void putCard(Card card, string Color)
        {
			var response = await serverConnection.SendPlayCard(CurrentPlayerId, card);
			SetGame();
        }

        private async void SetGame()
		{
			var respondeString = await serverConnection.GetPlayerGameState(CurrentPlayerId);

			//json serializer to Game object and set it globaly
			Game = JsonConvert.DeserializeObject<Game>(respondeString);
            ShowPlayersInformation();
            Update();
            mainPanel.Refresh();
            handPanel.Refresh();
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

        private async void Button2_ClickAsync(object sender, EventArgs e)
        {
			var response = await serverConnection.SendEmptyPostAsync("/draw/undo");
			//SetGame();
		}

        private async void Button3_ClickAsync(object sender, EventArgs e)
        {
			var response = await serverConnection.SendEmptyPostAsync("/uno/undo");
            //SetGame();
        }
    }
}
