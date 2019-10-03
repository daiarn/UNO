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
            this.Skip = new System.Windows.Forms.Button();
            this.UNO = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(668, 69);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(120, 59);
            this.Draw.TabIndex = 0;
            this.Draw.Text = "Draw card";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // Skip
            // 
            this.Skip.Location = new System.Drawing.Point(668, 145);
            this.Skip.Name = "Skip";
            this.Skip.Size = new System.Drawing.Size(120, 59);
            this.Skip.TabIndex = 1;
            this.Skip.Text = "Skip";
            this.Skip.UseVisualStyleBackColor = true;
            this.Skip.Click += new System.EventHandler(this.Skip_Click);
            // 
            // UNO
            // 
            this.UNO.Location = new System.Drawing.Point(668, 223);
            this.UNO.Name = "UNO";
            this.UNO.Size = new System.Drawing.Size(120, 59);
            this.UNO.TabIndex = 2;
            this.UNO.Text = "UNO";
            this.UNO.UseVisualStyleBackColor = true;
            this.UNO.Click += new System.EventHandler(this.UNO_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(668, 403);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(120, 35);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.UNO);
            this.Controls.Add(this.Skip);
            this.Controls.Add(this.Draw);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.Button Skip;
        private System.Windows.Forms.Button UNO;
        private System.Windows.Forms.Button Exit;
    }
}