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
        private bool m_IsValidMovesLeft = true;

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

                if (!HighlightValidMoves())
                {
                    ShowGameOverMessage();
                }

                if (m_IsPlayingAgainstComputer && m_CurrentPlayer is Computer)
                {
                    ((Computer)m_CurrentPlayer).MoveRandomly(m_Board);
                    UpdateBoardUI();
                    SwitchPlayer();

                    if (!HighlightValidMoves())
                    {
                        ShowGameOverMessage();
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid move. Try again.", "Invalid Move", MessageBoxButtons.OK);
            }
        }

        private bool HighlightValidMoves()
        {
            bool hasValidMoves = false;

            for (int rowIndex = 0; rowIndex < m_Board.Height; rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_Board.Width; colIndex++)
                {
                    Coordinate cell = new Coordinate(rowIndex, colIndex);
                    Button boardButton = (Button)boardPanel.GetControlFromPosition(colIndex, rowIndex);

                    if (m_Board.Cell(cell) == '\0')
                    {
                        Coordinate?[] validEdges = BoardValidator.IdentifyAllEdges(cell, m_CurrentPlayer.Color, m_Board);

                        if (BoardValidator.CellIsValid(cell, m_CurrentPlayer.Color, validEdges, m_Board))
                        {
                            hasValidMoves = true;
                            boardButton.Enabled = true;
                            boardButton.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            boardButton.Enabled = false;
                            boardButton.BackColor = SystemColors.Control;
                        }
                    }
                    else
                    {
                        boardButton.Enabled = false;
                        boardButton.BackColor = SystemColors.Control;
                    }
                }
            }

            return hasValidMoves;
        }

        private void ShowGameOverMessage()
        {
            m_Board.CalculateScores(m_Player1, m_Player2);

            string winnerMessage;
            if (m_Player1.Score > m_Player2.Score)
            {
                winnerMessage = $"Black Won!! ({m_Player1.Score}/{m_Player2.Score})";
            }
            else if (m_Player2.Score > m_Player1.Score)
            {
                winnerMessage = $"White Won!! ({m_Player2.Score}/{m_Player1.Score})";
            }
            else
            {
                winnerMessage = $"It's a Tie!! ({m_Player1.Score}/{m_Player2.Score})";
            }

            DialogResult result = MessageBox.Show($"{winnerMessage}\nWould you like another round?",
                                                  "Othello",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                this.Close();
            }
        }

        private void RestartGame()
        {
            m_Board = new Board(m_Board.Width, m_Board.Height);
            m_CurrentPlayer = m_Player1;
            UpdateBoardUI(); 

            HighlightValidMoves();
        }

        private void SwitchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
            Text = $"{m_CurrentPlayer.Name}'s Turn";
        }
    }
}
