using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OthelloGameLogic;

namespace OthelloGameWPF
{
    public partial class FormBoardGame : Form
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Computer m_Computer;
        private Player m_CurrentPlayer;
        private bool m_IsPlayingAgainstComputer;

        public FormBoardGame(int boardSize, bool isPlayingAgainstComputer)
        {
            InitializeComponent();
            m_Board = new Board(boardSize, boardSize);
            m_IsPlayingAgainstComputer = isPlayingAgainstComputer;

            // Initialize players
            m_Player1 = new Player("Player 1", 0, eColor.Black);
            m_Player2 = isPlayingAgainstComputer ? (Player)new Computer("Computer", 0, eColor.White) : new Player("Player 2", 0, eColor.White);
            m_CurrentPlayer = m_Player1;

            InitializeBoardButtons(boardSize);
            UpdateBoardUI();
        }

        private void InitializeBoardButtons(int boardSize)
        {
            boardPanel.RowCount = boardSize;
            boardPanel.ColumnCount = boardSize;
            boardPanel.Controls.Clear();

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Button btn = new Button
                    {
                        Dock = DockStyle.Fill,
                        Tag = new Coordinate(i, j)
                    };
                    btn.Click += BoardButton_Click;
                    boardPanel.Controls.Add(btn, i, j);
                }
            }
        }

        private void UpdateBoardUI()
        {
            foreach (Button btn in boardPanel.Controls)
            {
                Coordinate cell = (Coordinate)btn.Tag;
                char piece = m_Board.Cell(cell);
                btn.Text = piece == '\0' ? "" : piece.ToString();
            }
            Text = $"{m_CurrentPlayer.Name}'s Turn";
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Coordinate cell = (Coordinate)clickedButton.Tag;

            if (m_Board.TrySetCell(m_CurrentPlayer.Color, cell))
            {
                UpdateBoardUI();
                SwitchPlayer();

                if (m_IsPlayingAgainstComputer && m_CurrentPlayer is Computer)
                {
                    ((Computer)m_CurrentPlayer).MoveRandomly(m_Board);
                    UpdateBoardUI();
                    SwitchPlayer();
                }
            }
            else
            {
                MessageBox.Show("Invalid move. Try again.", "Invalid Move", MessageBoxButtons.OK);
            }
        }

        private void SwitchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
            Text = $"{m_CurrentPlayer.Name}'s Turn";
        }
    }
}
