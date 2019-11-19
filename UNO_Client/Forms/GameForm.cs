using System;
using System.Drawing;
using System.Windows.Forms;
using UNO_Client.Models;
using Newtonsoft.Json;
using System.Threading;
using UNO_Client.Decorator;
using UNO_Client.Adapter;
using UNO_Client.State;
using System.Linq;
using UNO_Client.Flyweight;
using UNO_Client.Composite;

namespace UNO_Client.Forms
{
	public partial class GameForm : Form
	{
		public float[,] xyImage;

		private static System.Windows.Forms.Timer GameTimer;
		private readonly ConnectionInterface serverConnection;
		private static readonly SoundAdapter soundAdaptor = new SoundAdapter();
		private Game Game;
        private JoinPost joinPost;
        private StateContext stateContext;

		public GameForm(JoinPost joinPost)
		{
            this.joinPost = joinPost;
			serverConnection = new HttpAdapter("https://localhost:44331/api/game", joinPost.Id); // TODO: change url?
            stateContext = new StateContext();
			SetGame();
			Thread.Sleep(1000);
			InitializeComponent();
			SetGameTimer();
        }

		private void Draw_ClickAsync(object sender, EventArgs e)
		{
			serverConnection.SendDrawCard();
			SetGame();
		}

		private void GiveUp_Click(object sender, EventArgs e)
		{
			serverConnection.SendLeaveGame();
			SetGame();
		}

		private void UNO_Click(object sender, EventArgs e)
		{
			serverConnection.SendSayUNO();
			soundAdaptor.turnOnSoundEffect();

			SetGame();
		}

		private void Exit_Click(object sender, EventArgs e)
		{
			serverConnection.SendLeaveGame();
		}

		const float HandCardWidth = 80f;

		private void HandPanel_Paint(object sender, PaintEventArgs e)
		{
			if (Game != null)
			{
				var widthPerCard = HandCardWidth / 2;
				var handCount = Game.Gamestate.Hand.Count;

				var graphics = e.Graphics;
				float movePosition = 0f;
				xyImage = new float[handCount, 2];

				float width = (float) handPanel.Width;
				float height = (float) handPanel.Height;
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
                    if (card != null)
                    {
						var img = CardImageStore.GetImage(card);

                        xyImage[i, 0] = middlePoint + movePosition;
                        xyImage[i, 1] = 15f; //Recommended cards
                        movePosition += widthPerCard;

                        var dbWidth = HandCardWidth / (float)img.Width;
                        var dbHeight = dbWidth * img.Height;

                        graphics.DrawImage(img, new RectangleF(xyImage[i, 0], xyImage[i, 1], HandCardWidth, dbHeight));
                    }
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

				if (color == 0)
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
				var img = CardImageStore.GetImage(Game.Gamestate.Hand[i]);

				var dblFac = HandCardWidth / (float) img.Width;
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
		Bitmap blankCardImage = new Bitmap("..//..//CardImages//_blank.png");

		private void MainPanel_Paint(object sender, PaintEventArgs e)
		{
			if (activePlayerFont == null)
			{
				activePlayerFont = new Font(playerFont, FontStyle.Bold);
			}

			var graphics = e.Graphics;
			var width = (float) mainPanel.Width;
			var height = (float) mainPanel.Height;
			var middlePointX = width / 2f;
			var middlePointY = height / 2f;

			//var img = Game.TopCard.GetImage();

			float dbWidth = 80f;
			float dbFac = dbWidth / (float) blankCardImage.Width;
			var dbHeight = dbFac * blankCardImage.Height;

			RectangleF rect = new RectangleF(middlePointX - (dbWidth / 2), middlePointY - dbHeight - (dbHeight / 4), dbWidth, dbHeight);
			RectangleF rectToDecorate = new RectangleF(middlePointX - (dbWidth / 2) - 5, middlePointY - dbHeight - (dbHeight / 4), dbWidth + 10, dbHeight);
            RectangleF rectComposite = new RectangleF();

            Star star = new Star(rectComposite, graphics);
            GraphicComposite graphicComposite = new GraphicComposite();
            graphicComposite.Add(star); //TODO: make shit work

			Rect simpleRect = new DiagonalDecorator(new BorderDecorator(new BackgroundDecorator(new SimpleRect(rectToDecorate, graphics))));
			simpleRect.Draw();
			graphics.DrawImage(CardImageStore.GetImage(Game.Gamestate.ActiveCard), rect);

		}

		private void putCard(Card card, int Color)
		{
			serverConnection.SendPlayCard(card, Color);
			SetGame();
		}

		private async void SetGame()
		{
			var respondeString = await serverConnection.GetPlayerGameState();

			//json serializer to Game object and set it globaly
			Game = JsonConvert.DeserializeObject<Game>(respondeString);
			ShowPlayersInformation();
			Update();
			mainPanel.Refresh();
			handPanel.Refresh();
            UpdatePlayerState();
            UpdateGameCounters();
            stateContext.WritePlayerStatusInGame(this);
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
				string line = player.Name + " " + player.CardCount;
				infomration[i] = line;
			}
			return infomration;
		}
		private void ShowPlayersInformation()
		{
			var info = FormatPlayersInformation();
			PlayersInfo.Lines = info;
		}

        private void UpdatePlayerState()
        {
            int playerCount = Game.Gamestate.Players.Count();
            var scoreboardIndex = Game.Gamestate.ScoreboardIndex;

            if (scoreboardIndex > -1 && scoreboardIndex != playerCount - 1)
            {
                stateContext.setState(new WinningState());
                return;
            }

            if (scoreboardIndex == playerCount - 1)
            {
                stateContext.setState(new LosingState());
                return;
            }

            if (IsActive())
            {
                stateContext.setState(new PlayingState());
            }
            else
            {
                stateContext.setState(new WaitingState());
            }
        }

        public void ChangeLabelToPlaying()
        {
            PlayerTurn.Text = "Your turn";
        }

        public void ChangeLabelToWaiting()
        {
            PlayerTurn.Text = "Waiting for opponent turn";
        }

        public void ChangeLabelToWon()
        {
            PlayerTurn.Text = "You won the game";
        }

        public void ChangeLabeToLost()
        {
            PlayerTurn.Text = "You lost. Better luck next time";
        }

        private bool IsActive()
        {
            return Game.Gamestate.Index == Game.Gamestate.ActivePlayer;
        }
        private WinnerInfo CheckIfWinner()
        {
            int myIndex = Game.Gamestate.Index;
            WinnerInfo[] winners = Game.Gamestate.Winners;
            int count = Game.Gamestate.Players.Count;
            for (int i = 0; i < count; i++)
            {
                if (winners[i].Index == myIndex)
                {
                    return winners[i];
                }
            }
            return null;
        }
        private void UpdateGameCounters()
        {
            int zeroCounter = Game.Gamestate.ZeroCounter;
            int wildCounter = Game.Gamestate.WildCounter;

            string counterInfo = String.Format("Zero cards drawn in the game {0}\n Wild cards drawn in the game {1}\n", zeroCounter, wildCounter);

            CounterInformation.Text = counterInfo;
        }
		private void GameForm_Load(object sender, EventArgs e)
		{

		}

		private void Button2_ClickAsync(object sender, EventArgs e)
		{
			serverConnection.SendUndoDraw();
			//SetGame();
		}

		private void Button3_ClickAsync(object sender, EventArgs e)
		{
			serverConnection.SendUndoUNO();
			//SetGame();
		}

        private void PlayerTurn_Click(object sender, EventArgs e)
        {

        }
    }
}
