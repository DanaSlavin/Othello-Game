using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Player
    {
        private readonly eButtonColor r_PlayerColor;
        private readonly eButtonColor r_OpponentColor;

        private readonly bool r_isComputer;
        private readonly string r_name;

        private int m_score;
        private PossibleMoves m_MypossibleMoves;

        public Player(int i_playerNumber, bool i_isComputerPlayer)
        {
            m_score = 2;
            r_isComputer = i_isComputerPlayer;
            if (i_playerNumber == 1)
            {
                r_name = "Black Player";
                r_PlayerColor = eButtonColor.Black;
                r_OpponentColor = eButtonColor.White;
            }
            else
            {
                r_name = "White Player";
                r_PlayerColor = eButtonColor.White;
                r_OpponentColor = eButtonColor.Black;
            }

            m_MypossibleMoves = new PossibleMoves(r_PlayerColor, r_OpponentColor);
        }

        internal void CalculatePossibleMoves(Board i_gameBoard)
        {
            m_MypossibleMoves.GetPossibleMoves(i_gameBoard);
        }

        internal List<Coordinates> PossibleMoves
        {
            get
            {
                return m_MypossibleMoves.ListOfPossibleMoves;
            }
        }

        public bool isMove(Coordinates i_move)
        {
            bool isInList = false;
            foreach (Coordinates item in this.PossibleMoves)
            {
                if (i_move.Col == item.Col && i_move.Row == item.Row)
                {
                    isInList = true;
                }
            }

            return isInList;
        }

        public bool HasMoves()
        {
            bool isMoves = true;
            if (m_MypossibleMoves.m_ListPossibleMoves.Count == 0)
            {
                isMoves = false;
            }

            return isMoves;
        }

        public bool IsComputer()
        {
            bool isComputer = false;
            if (r_isComputer)
            {
                isComputer = true;
            }

            return isComputer;
        }

        public string Name
        {
            get
            {
                return r_name;
            }
        }

        public int Score
        {
            get
            {
                return m_score;
            }

            set
            {
                m_score = value;
            }
        }

        public void CountMySymbol(Board i_gameBoard)
        {
           int countSymbol = 0;
            for (int row = 0; row < i_gameBoard.Size; row++)
            {
                for(int col = 0; col < i_gameBoard.Size; col++)
                {
                    if (i_gameBoard.GameBoard[row, col].Color == r_PlayerColor)
                    {
                        countSymbol++;
                    }
                }
            }

            Score = countSymbol;
        }

        public eButtonColor CurrentColor
        {
            get
            {
                return r_PlayerColor;
            }
        }
    }
}
