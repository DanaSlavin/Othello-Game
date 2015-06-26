using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class GameLogic
    {        
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Player m_OponentPlayer;
        private bool m_isAgainstComputer;
        private Board m_gameBoard;
        private int m_boardSize;
        
        public GameLogic(int i_BoardSize, bool i_isComputerPlayer)
        {
            m_boardSize = i_BoardSize;
            m_gameBoard = new Board(m_boardSize);
            m_isAgainstComputer = i_isComputerPlayer;
            GetPlayers();
            m_CurrentPlayer = m_Player1;
            m_OponentPlayer = m_Player2;
        }

        public void ComputerMove()
        {
            Random rnd = new Random();
            int randomInput = rnd.Next(m_CurrentPlayer.PossibleMoves.Count);
            if (m_CurrentPlayer.HasMoves())
            {
                FlipSymbols(m_CurrentPlayer.PossibleMoves[randomInput]);
            }
        }

        public void FlipSymbols(Coordinates i_inputMove)
        {
            m_gameBoard.GameBoard[i_inputMove.Row, i_inputMove.Col].Color = m_CurrentPlayer.CurrentColor;
            foreach (Coordinates PlaceToFlip in m_gameBoard.GameBoard[i_inputMove.Row, i_inputMove.Col].m_CellsToFlip)
            {
                m_gameBoard.GameBoard[PlaceToFlip.Row, PlaceToFlip.Col].Color = m_CurrentPlayer.CurrentColor;
            }

            m_gameBoard.GameBoard[i_inputMove.Row, i_inputMove.Col].m_CellsToFlip.Clear();
        }

        public void GetPlayers()
        {
            m_Player1 = new Player(1, m_isAgainstComputer);
            m_Player2 = new Player(2, m_isAgainstComputer);
        }

        public void ChangeTurn()
        {
            Player SwapPlayer;
            m_OponentPlayer.CalculatePossibleMoves(m_gameBoard);
            if (m_OponentPlayer.HasMoves())
            {
                SwapPlayer = m_CurrentPlayer;
                m_CurrentPlayer = m_OponentPlayer;
                m_OponentPlayer = SwapPlayer;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player OpponentPlayer
        {
            get
            {
                return m_OponentPlayer;
            }
        }

        public Board GameBoard
        {
            get
            {
                return m_gameBoard;
            }
        }
    }
}
