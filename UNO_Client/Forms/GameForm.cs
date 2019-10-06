﻿using System;
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

namespace UNO_Client.Forms
{
    public partial class GameForm : Form
    {
        public float[,] xyImage;
        private const string BASE_URL = "api/game";


        private static readonly HttpClient client = new HttpClient();
        private Game game;
        private Player currentPlayer;

        public GameForm()
        {
            InitializeComponent();
        }

        private async void Draw_ClickAsync(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
            {
                { "playerId", currentPlayer.playerId.ToString() },
                { "somehting", "something" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BASE_URL + "/draw", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private async void GiveUp_Click(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
            {
                { "playerId", currentPlayer.playerId.ToString() },
                { "somehting", "something" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BASE_URL + "/leave", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private async void UNO_Click(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
            {
                { "playerId", currentPlayer.playerId.ToString() },
                { "somehting", "something" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BASE_URL + "/uno", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private async void Exit_Click(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
            {
                { "playerId", currentPlayer.playerId.ToString() },
                { "somehting", "something" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BASE_URL + "/leave", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        const float HandCardWidth = 80f;

        private void HandPanel_Paint(object sender, PaintEventArgs e)
        {
            var widthPerCard = HandCardWidth / 2;
            var handCount = currentPlayer.cards.Count;

            var graphics = e.Graphics;
            float movePosition = 0f;
            xyImage = new float[handCount, 2];
            
            float width = (float)handPanel.Width;
            float height = (float)handPanel.Height;
            float middlePoint = width / 4f;

            if (handCount * widthPerCard > (width - middlePoint))
            {
                middlePoint = width - (handCount * widthPerCard);

                if(middlePoint < 0)
                {
                    middlePoint = 0;
                    widthPerCard = width / handCount;
                }
            }

            for (int i = 0; i < handCount; i++)
            {
                var card = currentPlayer.cards[i];
                var img = card.GetImage(); 

                xyImage[i, 0] = middlePoint + movePosition;
                xyImage[i, 1] = 15f; //Recommended cards
                movePosition += widthPerCard;

                var dbWidth = HandCardWidth / (float)img.Width;
                var dbHeight = dbWidth * img.Height;

                graphics.DrawImage(img, new RectangleF(xyImage[i, 0], xyImage[i, 1], HandCardWidth, dbHeight));
            }
        }

        private void handPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int bild = HitTestCard(e.Location);
            if (bild != -1)
            {
                var card = currentPlayer.cards[bild];
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

        }

        int HitTestCard(Point loc)
        {
            var x = loc.X;
            var y = loc.Y;

            int bild = -1;
            for (int i = 0; i < currentPlayer.cards.Count; i++)
            {
                var img = currentPlayer.cards[i].GetImage();

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
        Bitmap blankCardImage = new Bitmap("kaks"); //TODO: FIX IT

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

            var img = game.topCard.GetImage();

            float dbWidth = 80f;
            float dbFac = dbWidth / (float)img.Width;
            var dbHeight = dbFac * img.Height;

            graphics.DrawImage(img, new RectangleF(middlePointX - dbWidth / 2f, (middlePointY + dbHeight) < height ? middlePointY : (height - dbHeight), dbWidth, dbHeight));

            var gama = Math.Acos(1.0 - (Math.Pow(width, 2) / (2.0 * Math.Pow(middlePointY, 2.0) + 0.5 * Math.Pow(width, 2.0))));

            var numOtherPlayers = game.players.Count;
            var distance = (TAU - gama) / (double)numOtherPlayers;

            var radius = Math.Min(middlePointX, middlePointY);
            var phase = -Math.PI / 2 + gama / 2;

            dbWidth = 60f;
            dbFac = dbWidth / (float)img.Width;
            dbHeight = dbFac * img.Height;

            for (int i = 1; i < numOtherPlayers; i++)
            {
                Player player = game.players[i];
                if (player.playerId == currentPlayer.playerId)
                {
                    i++;
                    player = game.players[i];
                }

                var p_X = middlePointX + radius * (float)Math.Cos(distance * i + phase);
                var p_Y = middlePointY - radius * (float)Math.Sin(distance * i + phase);

                var font = player.playerId == currentPlayer.playerId ? activePlayerFont : playerFont;
                var size = graphics.MeasureString(player.nick, font);
                graphics.DrawString(player.nick, font, Brushes.Black, p_X - size.Width / 2, p_Y);

                graphics.DrawImage(blankCardImage, p_X - dbWidth / 2, p_Y += size.Height, dbWidth, dbHeight);

                var numString = player.cards.Count.ToString();

                size = graphics.MeasureString(numString, font);

                graphics.DrawString(numString, font, Brushes.Black, p_X - size.Width / 2, p_Y + dbHeight / 2 - size.Height / 2);
            }
        }

        private async void putCard(Card card, string Color)
        {
            var values = new Dictionary<string, string>
            {
                { "playerId", currentPlayer.playerId.ToString() },
                { "color", card.Color },
                { "value", card.Value }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(BASE_URL + "/play", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private async void SetPlayer()
        {
            var responseString = await client.GetStringAsync(BASE_URL + "/playername");
            //json serializer to Player object and set it globaly
        }

        private async void SetGame()
        {
            var respondeString = await client.GetStringAsync(BASE_URL);
            //json serializer to Game object and set it globaly
        }
    }
}