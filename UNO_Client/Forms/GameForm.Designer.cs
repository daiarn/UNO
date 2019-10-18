namespace UNO_Client.Forms
{
    partial class GameForm
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
            this.Draw = new System.Windows.Forms.Button();
            this.GiveUp = new System.Windows.Forms.Button();
            this.UNO = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.handPanel = new System.Windows.Forms.Panel();
            this.PlayersInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(501, 56);
            this.Draw.Margin = new System.Windows.Forms.Padding(2);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(90, 48);
            this.Draw.TabIndex = 0;
            this.Draw.Text = "Draw card";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_ClickAsync);
            // 
            // GiveUp
            // 
            this.GiveUp.Location = new System.Drawing.Point(502, 108);
            this.GiveUp.Margin = new System.Windows.Forms.Padding(2);
            this.GiveUp.Name = "GiveUp";
            this.GiveUp.Size = new System.Drawing.Size(90, 48);
            this.GiveUp.TabIndex = 1;
            this.GiveUp.Text = "Give up";
            this.GiveUp.UseVisualStyleBackColor = true;
            this.GiveUp.Click += new System.EventHandler(this.GiveUp_Click);
            // 
            // UNO
            // 
            this.UNO.Location = new System.Drawing.Point(502, 160);
            this.UNO.Margin = new System.Windows.Forms.Padding(2);
            this.UNO.Name = "UNO";
            this.UNO.Size = new System.Drawing.Size(90, 48);
            this.UNO.TabIndex = 2;
            this.UNO.Text = "UNO";
            this.UNO.UseVisualStyleBackColor = true;
            this.UNO.Click += new System.EventHandler(this.UNO_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(501, 327);
            this.Exit.Margin = new System.Windows.Forms.Padding(2);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(90, 28);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(10, 11);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(487, 176);
            this.mainPanel.TabIndex = 4;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // handPanel
            // 
            this.handPanel.Location = new System.Drawing.Point(10, 192);
            this.handPanel.Margin = new System.Windows.Forms.Padding(2);
            this.handPanel.Name = "handPanel";
            this.handPanel.Size = new System.Drawing.Size(487, 162);
            this.handPanel.TabIndex = 5;
            this.handPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.HandPanel_Paint);
            this.handPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.handPanel_MouseClick);
            this.handPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.handPanel_MouseMove);
            // 
            // PlayersInfo
            // 
            this.PlayersInfo.Location = new System.Drawing.Point(615, 56);
            this.PlayersInfo.Multiline = true;
            this.PlayersInfo.Name = "PlayersInfo";
            this.PlayersInfo.ReadOnly = true;
            this.PlayersInfo.Size = new System.Drawing.Size(227, 173);
            this.PlayersInfo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(615, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Players information";
            // 
            // StartGame
            // 
            this.StartGame.Location = new System.Drawing.Point(502, 213);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(90, 48);
            this.StartGame.TabIndex = 8;
            this.StartGame.Text = "Start game";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 366);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayersInfo);
            this.Controls.Add(this.handPanel);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.UNO);
            this.Controls.Add(this.GiveUp);
            this.Controls.Add(this.Draw);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.Button GiveUp;
        private System.Windows.Forms.Button UNO;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel handPanel;
        private System.Windows.Forms.TextBox PlayersInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartGame;
    }
}