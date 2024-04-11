using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Viewmodels.Entities
{
    public class BoardTypes
    {
        private static readonly byte NUMBER_OF_ROWS = 8;
        private static readonly byte NUMBER_OF_COLUMNS = 8;

        public static byte[,] SmallestBoard()
        {
            byte[,] boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                        
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.EMPTY;
                    }
                }
            }

            return boardMatrix;
        }

        public static byte[,] MediumBoard()
        {
            byte[,] boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                        
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                        
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.EMPTY;
                    }
                }
            }

            return boardMatrix;
        }

        public static byte[,] FullBoard()
        {
            byte[,] boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }
                    else if (rowIndex == 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.WHITE_PIECE;
                        
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                        
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                       
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 3 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.RED_PIECE;
                        
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = (byte)PieceTypes.EMPTY;
                    }
                }
            }

            return boardMatrix;
        }

        public static byte[,] KingsBoard()
        {
            byte[,] boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                       
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                        
                    }
                    else if (rowIndex == 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                        
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 3 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = 0;
                    }
                }
            }

            return boardMatrix;
        }
    }

}
