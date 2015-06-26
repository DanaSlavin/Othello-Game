using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Board
    {
        private Cell[,] m_GameBoard;
        private int m_size;



        // $G$ DSN-003 (-5) Constructor should not include code, Init should be in an outside method.
        public Board(int i_BoardSize)
        {
            m_size = i_BoardSize;
            m_GameBoard = new Cell[i_BoardSize, i_BoardSize];
            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int column = 0; column < i_BoardSize; column++)
                {
                    m_GameBoard[row, column] = new Cell();
                }
            }

            m_GameBoard[(i_BoardSize / 2) - 1, (i_BoardSize / 2) - 1].Color = eButtonColor.White;
            m_GameBoard[i_BoardSize / 2, i_BoardSize / 2].Color = eButtonColor.White;
            m_GameBoard[i_BoardSize / 2, (i_BoardSize / 2) - 1].Color = eButtonColor.Black;
            m_GameBoard[(i_BoardSize / 2) - 1, i_BoardSize / 2].Color = eButtonColor.Black;
        }

        public Cell[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public int Size
        {
            get
            {
                return m_size;
            }

            set
            {
                m_size = value;
            }
        }
    }
}
