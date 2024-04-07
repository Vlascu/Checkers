using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Viewmodels.Entities
{
    public enum PieceTypes : byte
    {
        EMPTY = 0,
        WHITE_PIECE = 1,
        RED_PIECE = 2,
        WHITE_KING = 3,
        RED_KING = 4,
    }
}
