using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Program
    {
        public static void Main()
        {
            GameSettingsForm settings = new GameSettingsForm();
            settings.ShowDialog();
            if (settings.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                GameBoardForm board = new GameBoardForm(settings.BoardSize, settings.IsComputerPlayerSelected);
                board.ShowDialog();
            }
        }
    }
}
