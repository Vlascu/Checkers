using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Viewmodels.Entities
{
    public class GameSettings
    {
        private byte[,] loadedBoard;
        private bool multipleMoves;

        public GameSettings()
        {
            multipleMoves = false;
            loadedBoard = Board.GetDefaultBoard();
        }

        public bool MultipleMoves { get { return multipleMoves; } set { multipleMoves = value; } }
        public byte[,] LoadBoard{ get { return loadedBoard;  } set { loadedBoard = value; } }
    }
}
