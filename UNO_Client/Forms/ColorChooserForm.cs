﻿using System;
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
	public partial class ColorChooserForm : Form
	{
		public int Color;
		public ColorChooserForm()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void RadioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.Color = 4;
		}

		private void RadioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.Color = 3;
		}

		private void RadioButton3_CheckedChanged(object sender, EventArgs e)
		{
			this.Color = 1;
		}

		private void RadioButton4_CheckedChanged(object sender, EventArgs e)
		{
			this.Color = 2;
		}

		private void ColorChooserForm_Load(object sender, EventArgs e)
		{

		}
	}
}
