using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Project1
{
    public class GameBoardForm : Form
    {
        public const string k_GameTitle = "Othello";
        public const string k_AnotherRound = "Would you like another round?";
        private const int k_ButtonSquareSize = 50;
        private const int k_SpaceBetweenButtonSize = 2;
        private static int s_NumOfBlackPlayerWins = 0;
        private static int s_NumOfWhitePlayerWins = 0;

        private readonly Color k_DefaultColor = Color.LightGray;
        private readonly int m_NumOfRowsAndCol;

        private CellButton[,] m_Buttons;
        private int k_BoardStartPointHorizontal = 10;
        private int k_BoardStartPointVertical = 10;
        private GameLogic m_GameLogic;
        private bool m_IsComputerPlayer;
        
        public GameBoardForm(int i_size, bool i_isComputerPlayer)
        {
            this.Text = k_GameTitle;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            m_NumOfRowsAndCol = i_size;
            m_Buttons = new CellButton[i_size, i_size];
            m_IsComputerPlayer = i_isComputerPlayer;
            m_GameLogic = new GameLogic(m_NumOfRowsAndCol, m_IsComputerPlayer);
            initializeBoard();
            UpDateGameBoard();
        }

        private void initializeBoard()
        {

            // $G$ CSS-000 (-5) The variable names is not meaningful and understandable.x?? y??
            int x = k_BoardStartPointHorizontal;
            int y = k_BoardStartPointVertical;
            for (int i = 0; i < m_NumOfRowsAndCol; i++)
            {
                for (int j = 0; j < m_NumOfRowsAndCol; j++)
                {
                    m_Buttons[i, j] = new CellButton(i, j);
                    m_Buttons[i, j].Width = k_ButtonSquareSize;
                    m_Buttons[i, j].Height = k_ButtonSquareSize;
                    m_Buttons[i, j].Location = new Point(x, y);
                    m_Buttons[i, j].BackColor = k_DefaultColor;
                    m_Buttons[i, j].Enabled = false;
                    if(m_GameLogic.GameBoard.GameBoard[i, j].Color == eButtonColor.Black)
                    {
                        m_Buttons[i, j].BackColor = Color.Black;
                        m_Buttons[i, j].ForeColor = Color.White;

                        // $G$ CSS-999 (-5) If you use string as a condition --> then you should have use constant here.
                        m_Buttons[i, j].Text = "o";
                    }

                    if(m_GameLogic.GameBoard.GameBoard[i, j].Color == eButtonColor.White)
                    {
                        m_Buttons[i, j].BackColor = Color.White;
                        m_Buttons[i, j].ForeColor = Color.Black;
                        m_Buttons[i, j].Text = "o";
                    }

                    if(m_GameLogic.GameBoard.GameBoard[i, j].Color == eButtonColor.Gray)
                    {
                        // $G$ CSS-029 (-5) Bad code duplication.
                        m_Buttons[i, j].BackColor = Color.Gray;
                        m_Buttons[i, j].ForeColor = Color.Black;
                        m_Buttons[i, j].Text = "o";                      
                    }

                    x += k_ButtonSquareSize + k_SpaceBetweenButtonSize;
                    m_Buttons[i, j].Click += new EventHandler(OnGameButton_Click);
                    this.Controls.Add(m_Buttons[i, j]);
                }

                x = k_BoardStartPointHorizontal;
                y += k_ButtonSquareSize + k_SpaceBetweenButtonSize;
            }

            this.AutoSize = true;
            UpDateGameBoard();
        }
        
        private void OnGameButton_Click(object sender, EventArgs e)
        {
            int row = (sender as CellButton).Row;
            int column = (sender as CellButton).Col;
            if(m_Buttons[row, column].Enabled)
            {
                m_GameLogic.FlipSymbols(new Coordinates(row, column));
                m_GameLogic.ChangeTurn();
                UpDateGameBoard();
                if (m_IsComputerPlayer && m_GameLogic.CurrentPlayer.IsComputer())
                { 
                    m_GameLogic.ComputerMove();
                    m_GameLogic.ChangeTurn();
                    UpDateGameBoard();
                }

                CheckIfGameFinished();
            }
        }

        private void CheckIfGameFinished()
        {
            bool isEndOfGame = !m_GameLogic.CurrentPlayer.HasMoves() && !m_GameLogic.OpponentPlayer.HasMoves();
            if (isEndOfGame)
            {
                string gameOverMessage = null;
                bool isCurrentPlayerBlack = m_GameLogic.CurrentPlayer.CurrentColor == eButtonColor.Black;
                m_GameLogic.CurrentPlayer.CountMySymbol(m_GameLogic.GameBoard);
                m_GameLogic.OpponentPlayer.CountMySymbol(m_GameLogic.GameBoard);

                if(m_GameLogic.CurrentPlayer.Score > m_GameLogic.OpponentPlayer.Score)
                {
                    if (isCurrentPlayerBlack)
                    {
                        s_NumOfBlackPlayerWins++;
                    }
                    else
                    {
                        s_NumOfWhitePlayerWins++;
                    }

                    gameOverMessage = string.Format("{0} won!!! ({1}/{2}) ({3}/{4}) {5}{6}", m_GameLogic.CurrentPlayer.Name, m_GameLogic.CurrentPlayer.Score, m_GameLogic.OpponentPlayer.Score, s_NumOfWhitePlayerWins, s_NumOfBlackPlayerWins, Environment.NewLine, k_AnotherRound);
                }

                if (m_GameLogic.CurrentPlayer.Score < m_GameLogic.OpponentPlayer.Score)
                {
                    if (isCurrentPlayerBlack)
                    {
                        s_NumOfWhitePlayerWins++;
                    }
                    else
                    {
                        s_NumOfBlackPlayerWins++;
                    }

                    gameOverMessage = string.Format("{0} won!!! ({1}/{2}) ({3}/{4}) {5}{6}", m_GameLogic.OpponentPlayer.Name, m_GameLogic.OpponentPlayer.Score, m_GameLogic.CurrentPlayer.Score, s_NumOfWhitePlayerWins, s_NumOfBlackPlayerWins, Environment.NewLine, k_AnotherRound);
                }

                if (m_GameLogic.CurrentPlayer.Score == m_GameLogic.OpponentPlayer.Score)
                {
                    gameOverMessage = string.Format("Teko!!! ({0}/{1}) ({3}/{4}) {5}{6}", m_GameLogic.OpponentPlayer.Score, m_GameLogic.CurrentPlayer.Score, s_NumOfWhitePlayerWins, s_NumOfBlackPlayerWins, Environment.NewLine, k_AnotherRound);
                }

                if (MessageBox.Show(gameOverMessage, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    m_GameLogic = new GameLogic(m_NumOfRowsAndCol, m_IsComputerPlayer);
                    UpDateGameBoard();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void UpDateGameBoard()
        {
            m_GameLogic.CurrentPlayer.CalculatePossibleMoves(m_GameLogic.GameBoard);

            // $G$ CSS-000 (-5) The variable name is not meaningful and understandable .-> i,j??
            for (int i = 0; i < m_NumOfRowsAndCol; i++)
            {
                for(int j = 0; j < m_NumOfRowsAndCol; j++)
                {
                    if (m_GameLogic.CurrentPlayer.isMove(new Coordinates(i, j)))
                    {
                        m_Buttons[i, j].BackColor = Color.Gray;
                        m_Buttons[i, j].Enabled = true;
                        m_Buttons[i, j].Text = string.Empty;
                    }
                    else if (m_GameLogic.GameBoard.GameBoard[i, j].Color == eButtonColor.Black )
                    {
                        m_Buttons[i, j].BackColor = Color.Black;
                        m_Buttons[i, j].ForeColor = Color.White;
                        m_Buttons[i, j].Text = "o";
                        m_Buttons[i, j].Enabled = false;
                    }
                    else if (m_GameLogic.GameBoard.GameBoard[i, j].Color == eButtonColor.White)
                    {
                        m_Buttons[i, j].BackColor = Color.White;
                        m_Buttons[i, j].ForeColor = Color.Black;

                        // $G$ CSS-999 (-5) If you use string as a condition --> then you should have use constant here.
                        m_Buttons[i, j].Text = "o";
                        m_Buttons[i, j].Enabled = false;
                    }
                    else
                    {
                        m_Buttons[i, j].BackColor = k_DefaultColor;
                        m_Buttons[i, j].Enabled = false;
                        m_Buttons[i, j].Text = string.Empty;
                    }
                }
            }
        }
    }
}
