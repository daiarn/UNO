namespace UNO_Client.Forms
{
    partial class ParserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackStatistics = new System.Windows.Forms.Button();
            this.roman = new System.Windows.Forms.TextBox();
            this.decimalNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackStatistics
            // 
            this.BackStatistics.Location = new System.Drawing.Point(648, 380);
            this.BackStatistics.Name = "BackStatistics";
            this.BackStatistics.Size = new System.Drawing.Size(140, 58);
            this.BackStatistics.TabIndex = 0;
            this.BackStatistics.Text = "Back";
            this.BackStatistics.UseVisualStyleBackColor = true;
            this.BackStatistics.Click += new System.EventHandler(this.BackStatistics_Click);
            // 
            // roman
            // 
            this.roman.Location = new System.Drawing.Point(304, 121);
            this.roman.Name = "roman";
            this.roman.Size = new System.Drawing.Size(100, 20);
            this.roman.TabIndex = 1;
            // 
            // decimalNumber
            // 
            this.decimalNumber.Location = new System.Drawing.Point(304, 232);
            this.decimalNumber.Name = "decimalNumber";
            this.decimalNumber.ReadOnly = true;
            this.decimalNumber.Size = new System.Drawing.Size(100, 20);
            this.decimalNumber.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter roman number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Decimal number";
            // 
            // convert
            // 
            this.convert.Location = new System.Drawing.Point(304, 147);
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(100, 63);
            this.convert.TabIndex = 7;
            this.convert.Text = "Convert";
            this.convert.UseVisualStyleBackColor = true;
            this.convert.Click += new System.EventHandler(this.convert_Click);
            // 
            // ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.convert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decimalNumber);
            this.Controls.Add(this.roman);
            this.Controls.Add(this.BackStatistics);
            this.Name = "ParserForm";
            this.Text = "StatiscticsForm";
            this.Load += new System.EventHandler(this.StatiscticsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackStatistics;
        private System.Windows.Forms.TextBox roman;
        private System.Windows.Forms.TextBox decimalNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button convert;
    }
}