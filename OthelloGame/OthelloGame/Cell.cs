using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Cell
    {
        private bool m_isCellEmpty;
        private eButtonColor m_MyColor;
        public List<Coordinates> m_CellsToFlip;

        public Cell()
        {
            m_isCellEmpty = true;
            m_MyColor = eButtonColor.LightGray;
            m_CellsToFlip = new List<Coordinates>();
        }

        public eButtonColor Color
        {
            get
            {
                return m_MyColor;
            }

            set
            {
                m_isCellEmpty = false;
                m_MyColor = value;
            }
        }

        public bool IsCellEmpty
        {
            get
            {
                return m_isCellEmpty;
            }
        }
    }
}
