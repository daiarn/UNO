using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO_Client.Forms
{
    public partial class PlayForm : Form
    {
        public PlayForm()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            //MenuForm form = new MenuForm();
            this.Close();

            //form.ShowDialog();
        }
    }
}
