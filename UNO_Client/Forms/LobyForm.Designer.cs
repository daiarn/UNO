namespace UNO_Client.Forms
{
    partial class LobyForm
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
			this.Players = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.gameOptions = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Players
			// 
			this.Players.Location = new System.Drawing.Point(12, 26);
			this.Players.Multiline = true;
			this.Players.Name = "Players";
			this.Players.ReadOnly = true;
			this.Players.Size = new System.Drawing.Size(200, 240);
			this.Players.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Players online";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(248, 218);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 48);
			this.button1.TabIndex = 2;
			this.button1.Text = "Start game";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// gameOptions
			// 
			this.gameOptions.FormattingEnabled = true;
			this.gameOptions.Items.AddRange(new object[] {
            "Only numbers",
            "Finite deck",
            "Long game"});
			this.gameOptions.Location = new System.Drawing.Point(218, 46);
			this.gameOptions.Name = "gameOptions";
			this.gameOptions.Size = new System.Drawing.Size(120, 49);
			this.gameOptions.TabIndex = 3;
			this.gameOptions.SelectedIndexChanged += new System.EventHandler(this.gameOptions_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(219, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Game options";
			// 
			// LobyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 278);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gameOptions);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Players);
			this.Name = "LobyForm";
			this.Text = "LobyForm";
			this.Load += new System.EventHandler(this.LobyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Players;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox gameOptions;
        private System.Windows.Forms.Label label2;
    }
}