using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Project1
{
    internal class GameSettingsForm : Form
    {
        private const string k_PlayAgainstComputer = "Play against the computer";
        private const string k_PlayAgainstHouman = "Play against your friend";
        private const string k_GameSettingsTitle = "Othello - Game Settings";

        private Button m_ButtonBoardSize;
        private Button m_ButtonPlayAgainstComputer;
        private Button m_ButtonPlayAgaingHuman;

        private int m_BoardSizeIndxInArray;
        private int m_BoardSize = 6;
        private string[] k_BoardSizes = { "6x6", "8x8", "10x10", "12x12" };
        private bool m_IsComputerPlayer = false;

        public GameSettingsForm()
        {
            m_BoardSizeIndxInArray = 0;
            initializeScreen();
            initializeButtons();
        }

        private void initializeScreen()
        {
            this.Text = k_GameSettingsTitle;
            this.Size = new Size(400, 250);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void initializeButtons()
        {
            m_ButtonBoardSize = new Button();
            m_ButtonBoardSize.Text = string.Format("Board Size: {0} (click to increase)", k_BoardSizes[m_BoardSizeIndxInArray]);
            m_ButtonBoardSize.Width = 330;
            m_ButtonBoardSize.Height = 65;
            m_ButtonBoardSize.Location = new Point(30, 25);
            m_ButtonBoardSize.Click += new EventHandler(m_ButtonBoardSize_Click);
            this.Controls.Add(m_ButtonBoardSize);

            m_ButtonPlayAgainstComputer = new Button();
            m_ButtonPlayAgainstComputer.Text = k_PlayAgainstComputer;
            m_ButtonPlayAgainstComputer.Location = new Point(30, 120);
            m_ButtonPlayAgainstComputer.Height = 55;
            m_ButtonPlayAgainstComputer.Width = 140;
            m_ButtonPlayAgainstComputer.Click += new EventHandler(m_ButtonAgainst_Click);
            this.Controls.Add(m_ButtonPlayAgainstComputer);

            m_ButtonPlayAgaingHuman = new Button();
            m_ButtonPlayAgaingHuman.Text = k_PlayAgainstHouman;
            m_ButtonPlayAgaingHuman.Location = new Point(m_ButtonPlayAgainstComputer.Left + m_ButtonPlayAgainstComputer.Width + 40, m_ButtonBoardSize.Top + m_ButtonBoardSize.Height + 30);
            m_ButtonPlayAgaingHuman.Height = 55;
            m_ButtonPlayAgaingHuman.Width = 140;
            m_ButtonPlayAgaingHuman.Click += new EventHandler(m_ButtonAgainst_Click);
            this.Controls.Add(m_ButtonPlayAgaingHuman);
        }

        private void m_ButtonAgainst_Click(object sender, EventArgs e)
        {
            Button ClikedButton = sender as Button;

            if (ClikedButton != null)
            {
                if (ClikedButton.Text == k_PlayAgainstComputer)
                {
                    m_IsComputerPlayer = true;
                }
                else
                {
                    m_IsComputerPlayer = false;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void m_ButtonBoardSize_Click(object sender, EventArgs e)
        {
            this.m_BoardSizeIndxInArray++;
            this.m_BoardSize += 2;
            if (this.m_BoardSizeIndxInArray >= k_BoardSizes.Length)
            {
                this.m_BoardSizeIndxInArray = 0;
                this.m_BoardSize = 6;
            }

            m_ButtonBoardSize.Text = string.Format("Board Size: {0} (click to increase)", k_BoardSizes[m_BoardSizeIndxInArray]);
        }

        public bool IsComputerPlayerSelected
        {
            get
            {
                return m_IsComputerPlayer;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }
    }
}
