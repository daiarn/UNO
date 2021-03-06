﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UNO_Client.Interpreter;

namespace UNO_Client.Forms
{
	public partial class ParserForm : Form
    {
		private readonly Parser parser;
        public ParserForm()
        {
            InitializeComponent();
            //parser = new Parser();
        }

        private void BackStatistics_Click(object sender, EventArgs e)
        {
            this.Close();
            var formToShow = Application.OpenForms.Cast<Form>()
    .FirstOrDefault(c => c is MenuForm);
            if (formToShow != null)
            {
                formToShow.Show();
            }
        }

        private void StatiscticsForm_Load(object sender, EventArgs e)
        {

        }

        private void convert_Click(object sender, EventArgs e)
        {
            string text = roman.Text;
            Context context = new Context(text);
            parser.Interpret(context);
            decimalNumber.Text = context.Output.ToString();
        }
    }
}
