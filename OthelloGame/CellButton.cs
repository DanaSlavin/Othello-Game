using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Project1
{
    public class CellButton : Button
    {
        private int r_Row;
        private int r_Col;

        public CellButton(int i_row, int i_col)
        {
            r_Col = i_col;
            r_Row = i_row;
        }

        public int Row
        {
            get
            {
                return r_Row;
            }
        }

        public int Col
        {
            get
            {
                return r_Col;
            }
        }
    }
}
