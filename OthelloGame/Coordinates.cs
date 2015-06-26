using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Coordinates
    {
        private int m_row;
        private int m_col;

        public Coordinates(int i_row, int i_col)
        {
            m_row = i_row;
            m_col = i_col;
        }

        public int Row
        {
            get
            {
                return m_row;
            }

            set
            {
                m_row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_col;
            }

            set
            {
                m_col = value;
            }
        }
    }
}
