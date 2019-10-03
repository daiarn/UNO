using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNO_Client.Forms;

namespace UNO_Client
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            PlayForm form = new PlayForm();
            this.Hide();
            form.ShowDialog();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            StatiscticsForm form = new StatiscticsForm();
            form.ShowDialog();
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
