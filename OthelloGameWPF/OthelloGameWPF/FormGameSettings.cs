using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloGameWPF
{
    public partial class FormGameSettings : Form
    {
        private int boardSize = 6;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        private void btnBoardSize_Click(object sender, EventArgs e)
        {
            boardSize += 2;
            if (boardSize > 12)
            {
                boardSize = 6; // Reset to 6x6 after reaching 12x12
            }
            BoardSizeButton.Text = $"Board Size: {boardSize}x{boardSize} (click to increase)";
        }

        // Event handler to start the game against the computer
        private void btnPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Pass the board size and true (for playing against the computer)
            FormBoardGame boardGameForm = new FormBoardGame(boardSize, true);
            boardGameForm.ShowDialog();
            this.Close();
        }

        // Event handler to start the game against a friend
        private void btnPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Pass the board size and false (for playing against a friend)
            FormBoardGame boardGameForm = new FormBoardGame(boardSize, false);
            boardGameForm.ShowDialog();
            this.Close();
        }
    }
}
