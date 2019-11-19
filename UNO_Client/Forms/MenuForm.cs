using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNO_Client.Forms;
using UNO_Client.Models;

namespace UNO_Client
{
    public partial class MenuForm : Form
    {
        private const string BASE_URL = "https://localhost:44331/api/game"; //TODO: change this
        private static readonly HttpClient client = new HttpClient();
        public MenuForm()
        {
            InitializeComponent();
        }

        private async void Play_Click(object sender, EventArgs e)
        {
            string name = NametextBox.Text;
            string JsonString = "{\"Name\":\"" + name + "\"}";
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(BASE_URL + "/join", content);
            string message = await response.Content.ReadAsStringAsync();
            JoinPost joinPost = JsonConvert.DeserializeObject<JoinPost>(message);

			if (!joinPost.Success)
			{
				throw new NotImplementedException("Fix your client");
			}

            LobyForm form = new LobyForm(joinPost);
            form.Show();
            this.Hide();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            StatiscticsForm form = new StatiscticsForm();
            form.ShowDialog();
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
