using System;
using System.Drawing;
using System.Windows.Forms;
using OthelloGameLogic;

namespace OthelloGameWPF
{
    public partial class FormBoardGame : Form
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private bool m_IsPlayingAgainstComputer;
        private bool m_IsValidMovesLeft = true;

        public FormBoardGame(int i_BoardSize, bool i_IsPlayingAgainstComputer)
        {
            InitializeComponent();
            m_Board = new Board(i_BoardSize, i_BoardSize);
            m_IsPlayingAgainstComputer = i_IsPlayingAgainstComputer;
            m_Player1 = new Player("Player 1", 0, eColor.Black);

            if (i_IsPlayingAgainstComputer)
            {
                m_Player2 = new Computer("Computer", 0, eColor.White);
            }
            else
            {
                m_Player2 = new Player("Player 2", 0, eColor.White);
            }

            m_CurrentPlayer = m_Player1;
            Text = $"Othello - {m_CurrentPlayer.Color}'s Turn";

            InitializeBoardButtons(i_BoardSize);
            UpdateBoardUI();
            highlightValidMoves();
        }

        private void InitializeBoardButtons(int i_BoardSize)
        {
            boardPanel.Controls.Clear();
            boardPanel.RowCount = i_BoardSize;
            boardPanel.ColumnCount = i_BoardSize;

            boardPanel.RowStyles.Clear();
            boardPanel.ColumnStyles.Clear();

            for (int index = 0; index < i_BoardSize; index++)
            {
                boardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / i_BoardSize));
                boardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / i_BoardSize));
            }

            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        Tag = new Coordinate(rowIndex, colIndex)
                    };

                    button.UseCompatibleTextRendering = true;
                    button.Click += BoardButton_Click;
                    boardPanel.Controls.Add(button, colIndex, rowIndex);
                }
            }

            ResizeForm(i_BoardSize);
        }

        private void ResizeForm(int i_BoardSize)
        {
            int buttonSize = 50;
            int formPadding = 50;

            Width = i_BoardSize * buttonSize + formPadding;
            Height = i_BoardSize * buttonSize + formPadding + 50;

            boardPanel.Width = i_BoardSize * buttonSize;
            boardPanel.Height = i_BoardSize * buttonSize;
        }

        private void UpdateBoardUI()
        {
            foreach (Button button in boardPanel.Controls)
            {
                Coordinate cell = (Coordinate)button.Tag;
                char cellSign = m_Board.Cell(cell);

                if (cellSign == '\0')
                {
                    button.Text = "";
                    button.BackColor = SystemColors.Control;
                    button.ForeColor = SystemColors.Control;

                }
                else if (cellSign == 'x')
                {
                    button.Text = "o";
                    button.BackColor = Color.Black;
                    button.ForeColor = Color.White;

                }
                else if (cellSign == 'o')
                {
                    button.Text = "o";
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                }
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Coordinate cell = (Coordinate)clickedButton.Tag;

            if (m_Board.TrySetCell(m_CurrentPlayer.Color, cell))
            {
                completeTurn();

                if (m_IsPlayingAgainstComputer && m_CurrentPlayer is Computer)
                {
                    ((Computer)m_CurrentPlayer).MoveRandomly(m_Board);
                    completeTurn();
                }
            }
            else
            {
                MessageBox.Show("Invalid move. Try again.", "Invalid Move", MessageBoxButtons.OK);
            }
        }

        private void completeTurn()
        {
            UpdateBoardUI();

            switchPlayers();

            if (!highlightValidMoves())
            {
                showGameOverMessage();
            }
        }

        private bool highlightValidMoves()
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
                    }
                }
            }

            return hasValidMoves;
        }

        private void showGameOverMessage()
        {
            string winnerMessage;

            m_Board.CalculateScores(m_Player1, m_Player2);

            if (m_Player1.Score > m_Player2.Score)
            {
                m_Player1.WinsCount ++;
                winnerMessage = $"Black Won!! ({m_Player1.Score}/{m_Player2.Score}) ({m_Player1.WinsCount}/{m_Player2.WinsCount})";
            }
            else if (m_Player2.Score > m_Player1.Score)
            {
                m_Player2.WinsCount ++;
                winnerMessage = $"White Won!! ({m_Player2.Score}/{m_Player1.Score}) ({m_Player2.WinsCount}/{m_Player1.WinsCount})";
            }
            else
            {
                m_Player1.WinsCount ++;
                m_Player2.WinsCount ++;
                winnerMessage = $"It's a Tie!! ({m_Player1.Score}/{m_Player2.Score}) ({m_Player1.WinsCount}/{m_Player2.WinsCount})";
            }

            DialogResult result = MessageBox.Show($"{winnerMessage}\nWould you like another round?",
                                                  "Othello",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                restartGame();
            }
            else
            {
                Close();
            }
        }

        private void restartGame()
        {
            m_Board = new Board(m_Board.Width, m_Board.Height);
            m_CurrentPlayer = m_Player1;
            UpdateBoardUI();
            highlightValidMoves();
        }

        private bool hasValidMoves(Player i_Player)
        {
            bool hasValidMoves = false;

            for (int rowIndex = 0; rowIndex < m_Board.Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < m_Board.Width; columnIndex++)
                {
                    if (isValidCell(new Coordinate(columnIndex, rowIndex), i_Player))
                    {
                        hasValidMoves = true;
                    }
                }
            }

            return hasValidMoves;
        }

        private bool isValidCell(Coordinate i_CellCoordinate, Player i_Player)
        {
            return BoardValidator.CellIsValid(
                i_CellCoordinate,
                i_Player.Color,
                BoardValidator.IdentifyAllEdges(i_CellCoordinate, i_Player.Color, m_Board),
                m_Board);
        }

        private void switchPlayers()
        {
            if (m_CurrentPlayer == m_Player1 && hasValidMoves(m_Player2))
            {
                m_CurrentPlayer = m_Player2;
            }
            else if (m_CurrentPlayer != m_Player1 && hasValidMoves(m_Player1))
            {
                m_CurrentPlayer = m_Player1;

            }


            Text = $"Othello - {m_CurrentPlayer.Color}'s Turn";
        }
    }
}