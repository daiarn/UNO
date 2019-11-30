using System;
using System.Windows.Forms;
using UNO_Client.Adapter;
using UNO_Client.Forms;

namespace UNO_Client
{
	public partial class MenuForm : Form
	{
		private readonly IConnection ServerConnection;
		public MenuForm()
		{
			ServerConnection = new HttpAdapter("https://localhost:44331/api/game"); // TODO: maybe let user input this
			InitializeComponent();
		}

		private async void Play_Click(object sender, EventArgs e)
		{
			string name = NametextBox.Text;

			var response = await ServerConnection.SendJoinGame(name);
			if (!response.Success)
			{
				throw new NotImplementedException("(MenuForm.cs) Failed to join! " + response.Message);
			}
			ServerConnection.SetIdentifier(response.Id);

			LobyForm form = new LobyForm(ServerConnection);
			form.Show();
			this.Hide();
		}

		//private void Statistics_Click(object sender, EventArgs e)
		//{
		//	StatiscticsForm form = new StatiscticsForm();
		//	form.ShowDialog();
		//	this.Hide();
		//}

		private void Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void button4_Click(object sender, EventArgs e)
        {
            ParserForm form = new ParserForm();
            form.Show();
            this.Hide();
        }
    }
}
