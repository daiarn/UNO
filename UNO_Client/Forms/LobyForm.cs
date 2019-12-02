using System;
using System.Windows.Forms;
using UNO_Client.Adapter;
using UNO_Client.Models;

namespace UNO_Client.Forms
{
	public partial class LobyForm : Form
	{
		private readonly IConnection ServerConnection;
		private static Timer RefreshTimer;
		private GameState Gamestate;
		public LobyForm(IConnection connection)
		{
			ServerConnection = connection;

			_ = UpdateGameInfoAsync();

			InitializeComponent();
			BeginGameTimer();
		}

		private async void StartGame_ClickAsync(object sender, EventArgs e)
		{
			bool isOnlyNumbers = GameOptions.GetItemChecked(0);
			bool isFiniteDeck = GameOptions.GetItemChecked(1);

			var response = await ServerConnection.SendStartGame(isFiniteDeck, isOnlyNumbers, false);
			if (!response.Success)
			{
				throw new NotImplementedException("(LobbyForm.cs) Failed to start! " + response.Message);
			}

			OpenGameForm();
		}

		private void OpenGameForm()
		{
			RefreshTimer.Stop();
			RefreshTimer.Dispose();
			GameForm form = new GameForm(ServerConnection);
			Close();
			form.Show();
		}

		private async System.Threading.Tasks.Task UpdateGameInfoAsync()
		{
			Gamestate = await ServerConnection.GetPlayerGameStateAsync();

			if (Gamestate.GamePhase == GamePhase.Playing)
			{
				OpenGameForm();
			}
			else
			{
				UpdatePlayersInformation();
			}
		}

		private void UpdatePlayersInformation()
		{
			int count = Gamestate.Players.Count;
			string[] infomration = new string[count];
			for (int i = 0; i < count; i++)
			{
				var player = Gamestate.Players[i];
				infomration[i] = player.Name;
			}
			Players.Lines = infomration;
		}

		private void BeginGameTimer()
		{
			// Create a timer with a two second interval.
			RefreshTimer = new Timer();
			RefreshTimer.Tick += new EventHandler(OnTimedGameEventAsync);
			// Hook up the Elapsed event for the timer. 
			RefreshTimer.Interval = 2000;
			RefreshTimer.Start();
		}

		private async void OnTimedGameEventAsync(Object myObject, EventArgs myEventArgs)
		{
			RefreshTimer.Stop();
			await UpdateGameInfoAsync();

			if (Gamestate.GamePhase == GamePhase.WaitingForPlayers)
				RefreshTimer.Enabled = true;
		}
	}
}
