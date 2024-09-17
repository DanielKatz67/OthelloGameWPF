namespace OthelloGameWPF
{
    partial class FormGameSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.PlayAgainstComputerButton = new System.Windows.Forms.Button();
            this.PlayAgainstFriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Location = new System.Drawing.Point(12, 12);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(433, 61);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "Board Size: 6x6 (click to increase)";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            this.BoardSizeButton.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // PlayAgainstComputerButton
            // 
            this.PlayAgainstComputerButton.Location = new System.Drawing.Point(12, 92);
            this.PlayAgainstComputerButton.Name = "PlayAgainstComputerButton";
            this.PlayAgainstComputerButton.Size = new System.Drawing.Size(211, 61);
            this.PlayAgainstComputerButton.TabIndex = 1;
            this.PlayAgainstComputerButton.Text = "Play against the computer";
            this.PlayAgainstComputerButton.UseVisualStyleBackColor = true;
            this.PlayAgainstComputerButton.Click += new System.EventHandler(this.buttonPlayAgainstComputer_Click);
            // 
            // PlayAgainstFriendButton
            // 
            this.PlayAgainstFriendButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PlayAgainstFriendButton.Location = new System.Drawing.Point(234, 92);
            this.PlayAgainstFriendButton.Name = "PlayAgainstFriendButton";
            this.PlayAgainstFriendButton.Size = new System.Drawing.Size(211, 61);
            this.PlayAgainstFriendButton.TabIndex = 2;
            this.PlayAgainstFriendButton.Text = "Play against your friend";
            this.PlayAgainstFriendButton.UseVisualStyleBackColor = true;
            this.PlayAgainstFriendButton.Click += new System.EventHandler(this.buttonPlayAgainstFriend_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 166);
            this.Controls.Add(this.PlayAgainstFriendButton);
            this.Controls.Add(this.PlayAgainstComputerButton);
            this.Controls.Add(this.BoardSizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSettings";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button PlayAgainstComputerButton;
        private System.Windows.Forms.Button PlayAgainstFriendButton;
    }
}