using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    internal class PossibleMoves
    {
        private eButtonColor m_MyColor;
        private eButtonColor m_OpponentColor;
        public List<Coordinates> m_ListPossibleMoves;

        public PossibleMoves(eButtonColor i_myColor, eButtonColor i_opponentColor)
        {
            m_MyColor = i_myColor;
            m_OpponentColor = i_opponentColor;
            m_ListPossibleMoves = new List<Coordinates>();
        }

        public void GetPossibleMoves(Board i_gameBoard)
        {
            m_ListPossibleMoves.Clear();
            for (int row = 0; row < i_gameBoard.Size; row++)
            {
                bool isUpFlip, isDownFlip, isLeftFlip, isRightFlip, isUpRightFlip, isUpLeftFlip, isDownLeftFlip, isDownRightFlip;
                for (int col = 0; col < i_gameBoard.Size; col++)
                {
                    i_gameBoard.GameBoard[row, col].m_CellsToFlip.Clear();
                    if (i_gameBoard.GameBoard[row, col].IsCellEmpty)
                    {
                            isUpFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "up");
                            isLeftFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "left");
                            isRightFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "right");
                            isDownFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "down");
                            isUpRightFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "upright");
                            isUpLeftFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "upleft");
                            isDownLeftFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "downleft");
                            isDownRightFlip = GenericCheck(m_MyColor, m_OpponentColor, row, col, i_gameBoard, "downright");
                        if (isUpFlip || isDownFlip || isLeftFlip || isRightFlip || isUpRightFlip || isUpLeftFlip || isDownLeftFlip || isDownRightFlip)
                        {
                            m_ListPossibleMoves.Add(new Coordinates(row, col));
                        }
                    }
                }
            }
        }
        
        private bool GenericCheck(eButtonColor i_MySymbol, eButtonColor i_OpponentSymbol, int i_row, int i_col, Board i_gameBoard, string i_direction)
        {
            bool isSeenOtherPlayer = false;
            bool isSeenSelfAndLegal = false;
            int deltaPositionRow = i_row;
            int deltaPositionCol = i_col;
            int addCellToFlipCounter = 0;
            incrementDelta(ref deltaPositionRow, ref deltaPositionCol, i_direction);

            while ((deltaPositionRow >= 0) && (deltaPositionRow < i_gameBoard.Size) && (deltaPositionCol >= 0) 
                && (deltaPositionCol < i_gameBoard.Size))
            {
                if (i_gameBoard.GameBoard[deltaPositionRow, deltaPositionCol].IsCellEmpty )
                {
                    break;
                }

                if (i_gameBoard.GameBoard[deltaPositionRow, deltaPositionCol].Color == i_MySymbol && !isSeenOtherPlayer)
                {
                    break;
                }

                if (i_gameBoard.GameBoard[deltaPositionRow, deltaPositionCol].Color == i_OpponentSymbol)
                {
                    isSeenOtherPlayer = true;
                }

                if (i_gameBoard.GameBoard[deltaPositionRow, deltaPositionCol].Color == i_MySymbol && isSeenOtherPlayer)
                {
                    isSeenSelfAndLegal = true;
                    break;
                }

                if (isSeenOtherPlayer && !isSeenSelfAndLegal)
                {
                    addCellToFlip(i_gameBoard, i_row, i_col, deltaPositionRow, deltaPositionCol);
                    addCellToFlipCounter++;
                }

                incrementDelta(ref deltaPositionRow, ref deltaPositionCol, i_direction);
            }

            if (!isSeenSelfAndLegal)
            {
                int indexToRemove = i_gameBoard.GameBoard[i_row, i_col].m_CellsToFlip.Count - addCellToFlipCounter;
                i_gameBoard.GameBoard[i_row, i_col].m_CellsToFlip.RemoveRange(indexToRemove, addCellToFlipCounter);
            }

            return isSeenSelfAndLegal;
        }

        private void incrementDelta(ref int io_deltaPositionRow, ref int io_deltaPositionCol, string i_direction)
        {
            switch (i_direction)
            {
                case "up":
                    io_deltaPositionRow--;
                    break;
                case "down":
                    io_deltaPositionRow++;
                    break;
                case "left":
                    io_deltaPositionCol--;
                    break;
                case "right":
                    io_deltaPositionCol++;
                    break;
                case "upright":
                    io_deltaPositionRow--;
                    io_deltaPositionCol++;
                    break;
                case "upleft":
                    io_deltaPositionRow--;
                    io_deltaPositionCol--;
                    break;
                case "downright":
                    io_deltaPositionRow++;
                    io_deltaPositionCol++;
                    break;
                case "downleft":
                    io_deltaPositionRow++;
                    io_deltaPositionCol--;
                    break;
            }
        }

        private void addCellToFlip(Board i_gameBoard, int i_Therow, int i_Thecol, int i_RowToBeFlipped, int i_ColToBeFlipped)
        {
            Coordinates CellToFlip = new Coordinates(i_RowToBeFlipped, i_ColToBeFlipped);
            i_gameBoard.GameBoard[i_Therow, i_Thecol].m_CellsToFlip.Add(CellToFlip);
        }

        public List<Coordinates> ListOfPossibleMoves
        {
            get
            {
                return m_ListPossibleMoves;
            }
        }
    }
}
