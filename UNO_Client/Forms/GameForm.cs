using System;
using System.Drawing;
using System.Windows.Forms;
using UNO_Client.Models;
using UNO_Client.Decorator;
using UNO_Client.Adapter;
using UNO_Client.State;
using System.Linq;
using UNO_Client.Flyweight;
using System.Threading.Tasks;

namespace UNO_Client.Forms
{
	public partial class GameForm : Form
	{
		public float[,] xyImage;

		private static Timer RefreshTimer;
		private static readonly SoundAdapter SoundAdaptor = new SoundAdapter();
		private readonly IConnection ServerConnection;
		private readonly StateContext StateContext = new StateContext();
		private GameState Gamestate;

		public GameForm(IConnection connection)
		{
			ServerConnection = connection;
			_ = UpdateGameStateAsync();

			InitializeComponent();
			BeginGameTimer();
		}

		private async void Draw_Click(object sender, EventArgs e)
		{
			await ServerConnection.SendDrawCardAsync();
			_ = UpdateGameStateAsync();
		}

		private async void GiveUp_Click(object sender, EventArgs e)
		{
			await ServerConnection.SendLeaveGameAsync();
			_ = UpdateGameStateAsync();
		}

		private async void UNO_Click(object sender, EventArgs e)
		{
			await ServerConnection.SendSayUnoAsync();
			SoundAdaptor.turnOnSoundEffect();

			_ = UpdateGameStateAsync();
		}

		private void Exit_Click(object sender, EventArgs e)
		{
			ServerConnection.SendLeaveGameAsync();
			Close();
		}

		private async Task UpdateGameStateAsync()
		{
			Gamestate = await ServerConnection.GetPlayerGameStateAsync();

			ShowPlayersInformation();

			Update();
			mainPanel.Refresh();
			handPanel.Refresh();

			UpdatePlayerState();
			UpdateGameCounters();
			StateContext.WritePlayerStatusInGame(this);
		}

		const float HandCardWidth = 80f;

		private void HandPanel_Paint(object sender, PaintEventArgs e)
		{
			if (Gamestate != null)
			{
				var widthPerCard = HandCardWidth / 2;
				var handCount = Gamestate.Hand.Count;

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
					var card = Gamestate.Hand[i];
					if (card != null)
					{
						var img = CardImageStore.GetImage(card);

						xyImage[i, 0] = middlePoint + movePosition;
						xyImage[i, 1] = 15f; // Recommended cards
						movePosition += widthPerCard;

						var dbWidth = HandCardWidth / (float) img.Width;
						var dbHeight = dbWidth * img.Height;

						graphics.DrawImage(img, new RectangleF(xyImage[i, 0], xyImage[i, 1], HandCardWidth, dbHeight));
					}
				}
			}
		}

		private async void HandPanel_MouseClick(object sender, MouseEventArgs e)
		{
			int bild = HitTestCard(e.Location);
			if (bild != -1)
			{
				var card = Gamestate.Hand[bild];
				var color = card.Color;

				if (color == 0)
				{
					var chooser = new ColorChooserForm();
					chooser.ShowDialog(this);
					color = chooser.Color;
				}

				await ServerConnection.SendPlayCardAsync(card, color);
				_ = UpdateGameStateAsync();
			}
		}

		int HitTestCard(Point loc)
		{
			var x = loc.X;
			var y = loc.Y;

			int bild = -1;
			for (int i = 0; i < Gamestate.Hand.Count; i++)
			{
				var img = CardImageStore.GetImage(Gamestate.Hand[i]);

				var dblFac = HandCardWidth / (float) img.Width;
				var dblHeight = dblFac * img.Height;

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

			float dbWidth = 80f;
			float dbFac = dbWidth / (float) blankCardImage.Width;
			var dbHeight = dbFac * blankCardImage.Height;

			RectangleF rect = new RectangleF(middlePointX - (dbWidth / 2), middlePointY - dbHeight - (dbHeight / 4), dbWidth, dbHeight);
			RectangleF rectToDecorate = new RectangleF(middlePointX - (dbWidth / 2) - 5, middlePointY - dbHeight - (dbHeight / 4), dbWidth + 10, dbHeight);
			Rect simpleRect = new DiagonalDecorator(new BorderDecorator(new BackgroundDecorator(new SimpleRect(rectToDecorate, graphics))));
			simpleRect.Draw();
			graphics.DrawImage(CardImageStore.GetImage(Gamestate.ActiveCard), rect);

			if(stateContext.GetState() is WinningState)
			{
				PrintStars(graphics);
			}
		}

		private void BeginGameTimer()
		{
			// Create a timer with a two second interval.
			RefreshTimer = new System.Windows.Forms.Timer();
			RefreshTimer.Tick += new EventHandler(OnTimedGameEvent);
			// Hook up the Elapsed event for the timer. 
			RefreshTimer.Interval = 2000;
			RefreshTimer.Start();
		}

		private async void OnTimedGameEvent(Object myObject, EventArgs myEventArgs)
		{
			RefreshTimer.Stop();
			await UpdateGameStateAsync();
			RefreshTimer.Enabled = true;
		}

		private void ShowPlayersInformation()
		{
			int count = Gamestate.Players.Count;
			string[] infomration = new string[count];
			for (int i = 0; i < count; i++)
			{
				var player = Gamestate.Players[i];
				string line = player.Name + " " + player.CardCount;
				infomration[i] = line;
			}
			PlayersInfo.Lines = infomration;
		}

		private void UpdatePlayerState()
		{
			int playerCount = Gamestate.Players.Count();
			var scoreboardIndex = Gamestate.ScoreboardIndex;

			if (scoreboardIndex > -1 && scoreboardIndex != playerCount - 1)
			{
				StateContext.setState(new WinningState());
				if (!soundOn)
				{
					soundAdaptor.turnOnSoundEffect();
					soundOn = true;
				}
				return;
			}

			if (scoreboardIndex == playerCount - 1)
			{
				StateContext.setState(new LosingState());
				return;
			}

			if (IsActive())
			{
				StateContext.setState(new PlayingState());
			}
			else
			{
				StateContext.setState(new WaitingState());
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
			return Gamestate.Index == Gamestate.ActivePlayer;
		}
		
		private void PrintStars(Graphics Graphics)
		{
			// Create stars
			Star star1 = new Star(30, 30, Color.Red, Graphics);
			Star star2 = new Star(370, 20, Color.Blue, Graphics);
			Star star3 = new Star(350, 90, Color.Green, Graphics);
			Star star4 = new Star(110, 35, Color.Pink, Graphics);
			Star star5 = new Star(310, 70, Color.Yellow, Graphics);
			Star star6 = new Star(175, 10, Color.Violet, Graphics);
			Star star7 = new Star(350, 50, Color.LimeGreen, Graphics);
			Star star8 = new Star(300, 250, Color.Orange, Graphics);
			Star star9 = new Star(410, 80, Color.Lavender, Graphics);

			// Create composite
			GraphicComposite graphicCompositeMain = new GraphicComposite();
			GraphicComposite graphicComposite1 = new GraphicComposite();
			GraphicComposite graphicComposite2 = new GraphicComposite();
			GraphicComposite graphicComposite3 = new GraphicComposite();

			// Add stars to composite
			graphicComposite1.Add(star1);
			graphicComposite1.Add(star3);
			graphicComposite1.Add(star5);

			graphicComposite2.Add(star2);
			graphicComposite2.Add(star4);
			graphicComposite2.Add(star6);

			graphicComposite3.Add(star7);
			graphicComposite3.Add(star8);
			graphicComposite3.Add(star9);

			// Add composite to main
			graphicCompositeMain.Add(graphicComposite1);
			graphicCompositeMain.Add(graphicComposite2);
			graphicCompositeMain.Add(graphicComposite3);

			// Paint
			graphicCompositeMain.Paint();
		}

		private void UpdateGameCounters()
		{
			int zeroCounter = Gamestate.ZeroCounter;
			int wildCounter = Gamestate.WildCounter;

			string counterInfo = String.Format("Zero cards drawn in the game {0}\n Wild cards drawn in the game {1}\n", zeroCounter, wildCounter);

			CounterInformation.Text = counterInfo;
		}

		private void UndoDraw_Click(object sender, EventArgs e)
		{
			//serverConnection.SendUndoDraw();
			//_ = UpdateGameStateAsync();
		}

		private void UndoUno_Click(object sender, EventArgs e)
		{
			//serverConnection.SendUndoUNO();
			//_ = UpdateGameStateAsync();
		}
	}
}
