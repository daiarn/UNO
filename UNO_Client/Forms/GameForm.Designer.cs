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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.CounterInformation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerTurn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(618, 67);
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
            this.GiveUp.Location = new System.Drawing.Point(618, 117);
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
            this.UNO.Location = new System.Drawing.Point(618, 169);
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
            this.Exit.Location = new System.Drawing.Point(618, 332);
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
            this.mainPanel.Location = new System.Drawing.Point(11, 48);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(603, 314);
            this.mainPanel.TabIndex = 4;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // handPanel
            // 
            this.handPanel.Location = new System.Drawing.Point(10, 195);
            this.handPanel.Margin = new System.Windows.Forms.Padding(2);
            this.handPanel.Name = "handPanel";
            this.handPanel.Size = new System.Drawing.Size(604, 165);
            this.handPanel.TabIndex = 5;
            this.handPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.HandPanel_Paint);
            this.handPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.handPanel_MouseClick);
            this.handPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.handPanel_MouseMove);
            // 
            // PlayersInfo
            // 
            this.PlayersInfo.Location = new System.Drawing.Point(712, 219);
            this.PlayersInfo.Margin = new System.Windows.Forms.Padding(2);
            this.PlayersInfo.Multiline = true;
            this.PlayersInfo.Name = "PlayersInfo";
            this.PlayersInfo.ReadOnly = true;
            this.PlayersInfo.Size = new System.Drawing.Size(171, 141);
            this.PlayersInfo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(712, 204);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Players information";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(618, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Undo Draw";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_ClickAsync);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(618, 251);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Undo UNO";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_ClickAsync);
            // 
            // CounterInformation
            // 
            this.CounterInformation.Location = new System.Drawing.Point(712, 61);
            this.CounterInformation.Margin = new System.Windows.Forms.Padding(2);
            this.CounterInformation.Multiline = true;
            this.CounterInformation.Name = "CounterInformation";
            this.CounterInformation.ReadOnly = true;
            this.CounterInformation.Size = new System.Drawing.Size(171, 141);
            this.CounterInformation.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(712, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Game information";
            // 
            // PlayerTurn
            // 
            this.PlayerTurn.AutoSize = true;
            this.PlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTurn.Location = new System.Drawing.Point(229, 9);
            this.PlayerTurn.Name = "PlayerTurn";
            this.PlayerTurn.Size = new System.Drawing.Size(147, 31);
            this.PlayerTurn.TabIndex = 13;
            this.PlayerTurn.Text = "PlayerTurn";
            this.PlayerTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PlayerTurn.Click += new System.EventHandler(this.PlayerTurn_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 366);
            this.Controls.Add(this.PlayerTurn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CounterInformation);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.handPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayersInfo);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox CounterInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PlayerTurn;
    }
}