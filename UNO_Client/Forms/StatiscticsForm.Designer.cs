namespace UNO_Client.Forms
{
    partial class StatiscticsForm
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
            this.gamesPlayed = new System.Windows.Forms.TextBox();
            this.gamesWon = new System.Windows.Forms.TextBox();
            this.gamesLost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            // gamesPlayed
            // 
            this.gamesPlayed.Location = new System.Drawing.Point(304, 121);
            this.gamesPlayed.Name = "gamesPlayed";
            this.gamesPlayed.ReadOnly = true;
            this.gamesPlayed.Size = new System.Drawing.Size(100, 20);
            this.gamesPlayed.TabIndex = 1;
            // 
            // gamesWon
            // 
            this.gamesWon.Location = new System.Drawing.Point(304, 176);
            this.gamesWon.Name = "gamesWon";
            this.gamesWon.ReadOnly = true;
            this.gamesWon.Size = new System.Drawing.Size(100, 20);
            this.gamesWon.TabIndex = 2;
            // 
            // gamesLost
            // 
            this.gamesLost.Location = new System.Drawing.Point(304, 232);
            this.gamesLost.Name = "gamesLost";
            this.gamesLost.ReadOnly = true;
            this.gamesLost.Size = new System.Drawing.Size(100, 20);
            this.gamesLost.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total games played";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Games won";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Games lost";
            // 
            // StatiscticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamesLost);
            this.Controls.Add(this.gamesWon);
            this.Controls.Add(this.gamesPlayed);
            this.Controls.Add(this.BackStatistics);
            this.Name = "StatiscticsForm";
            this.Text = "StatiscticsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackStatistics;
        private System.Windows.Forms.TextBox gamesPlayed;
        private System.Windows.Forms.TextBox gamesWon;
        private System.Windows.Forms.TextBox gamesLost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}