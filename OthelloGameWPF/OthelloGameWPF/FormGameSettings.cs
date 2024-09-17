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
        private int m_boardSize = 6;

        public FormGameSettings()
        {
            InitializeComponent();
            UpdateBoardSizeButtonText();
        }

        public int BoardSize
        {
            get
            {
                return m_boardSize;
            }
            set
            {
                m_boardSize = value;
                UpdateBoardSizeButtonText();
            }
        }

        private void UpdateBoardSizeButtonText()
        {
            this.BoardSizeButton.Text = $"Board Size: {this.BoardSize}x{this.BoardSize} (click to increase)";
        }
    }
}
