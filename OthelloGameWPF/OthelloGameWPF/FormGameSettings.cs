using System;
using System.Windows.Forms;

namespace OthelloGameWPF
{
    public partial class FormGameSettings : Form
    {
        private int m_BoardSize = 6;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            m_BoardSize += 2;
            if (m_BoardSize > 12)
            {
                m_BoardSize = 6;
            }
            BoardSizeButton.Text = $"Board Size: {m_BoardSize}x{m_BoardSize} (click to increase)";
        }

        private void buttonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            Hide();
            FormBoardGame boardGameForm = new FormBoardGame(m_BoardSize, true);
            boardGameForm.ShowDialog();
            Close();
        }

        private void buttonPlayAgainstFriend_Click(object sender, EventArgs e)
        {
            Hide();
            FormBoardGame boardGameForm = new FormBoardGame(m_BoardSize, false);
            boardGameForm.ShowDialog();
            Close();
        }
    }
}
