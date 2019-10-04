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
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string playerName = Nick_textBox.Text;
            //do post
        }

        private void NameForm_Load(object sender, EventArgs e)
        {

        }

        private void Nick_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
