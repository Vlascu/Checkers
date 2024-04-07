using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Viewmodels.Entities
{
    internal class Board
    {
        private static readonly byte NUMBER_OF_ROWS = 8;
        private static readonly byte NUMBER_OF_COLUMNS = 8;
        private byte[,] boardMatrix;
        private byte numberOfRedPieces;
        private byte numberOfWhitePieces;

        Board(byte option)
        {
            numberOfWhitePieces = 0;
            numberOfRedPieces = 0;
            boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
            InitMatrix(option);
        }

        public List<Tuple<byte, byte>> GetAvailableMoves(byte row, byte column)
        {
            List<Tuple<byte, byte>> possbileMoves = new List<Tuple<byte, byte>>();

            if (boardMatrix[row, column] == 3 || boardMatrix[row, column] == 4)
            {
                if (boardMatrix[row, column] == 3)
                {
                    //Top-left
                    if (row > 0 && column > 0)
                    {
                        if (boardMatrix[row - 1, column - 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column - 1)));
                        }
                        else if (row > 1 && column > 1 && (boardMatrix[row - 1, column - 1] == 2 || boardMatrix[row - 1, column - 1] == 4))
                        {
                            if (boardMatrix[row - 2, column - 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column - 2)));
                            }
                        }
                    }

                    // Top-right
                    if (row > 0 && column < NUMBER_OF_COLUMNS - 1)
                    {
                        if (boardMatrix[row - 1, column + 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column + 1)));
                        }
                        else if (row > 1 && column < NUMBER_OF_COLUMNS - 2 && (boardMatrix[row - 1, column + 1] == 2 || boardMatrix[row - 1, column + 1] == 4))
                        {
                            if (boardMatrix[row - 2, column + 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column + 2)));
                            }
                        }
                    }

                    // Bottom-left
                    if (row < NUMBER_OF_ROWS - 1 && column > 0)
                    {
                        if (boardMatrix[row + 1, column - 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column - 1)));
                        }
                        else if (row < NUMBER_OF_ROWS - 2 && column > 1 && (boardMatrix[row + 1, column - 1] == 2 || boardMatrix[row + 1, column - 1] == 4))
                        {
                            if (boardMatrix[row + 2, column - 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column - 2)));
                            }
                        }
                    }

                    // Bottom-right
                    if (row < NUMBER_OF_ROWS - 1 && column < NUMBER_OF_COLUMNS - 1)
                    {
                        if (boardMatrix[row + 1, column + 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column + 1)));
                        }
                        else if (row < NUMBER_OF_ROWS - 2 && column < NUMBER_OF_COLUMNS - 2 && (boardMatrix[row + 1, column + 1] == 2 || boardMatrix[row + 1, column + 1] == 4))
                        {
                            if (boardMatrix[row + 2, column + 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column + 2)));
                            }
                        }
                    }
                } else if (boardMatrix[row, column] == 4)
                {
                    if (row > 0 && column > 0)
                    {
                        if (boardMatrix[row - 1, column - 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column - 1)));
                        }
                        else if (row > 1 && column > 1 && (boardMatrix[row - 1, column - 1] == 1 || boardMatrix[row - 1, column - 1] == 3))
                        {
                            if (boardMatrix[row - 2, column - 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column - 2)));
                            }
                        }
                    }

                    // Top-right
                    if (row > 0 && column < NUMBER_OF_COLUMNS - 1)
                    {
                        if (boardMatrix[row - 1, column + 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column + 1)));
                        }
                        else if (row > 1 && column < NUMBER_OF_COLUMNS - 2 && (boardMatrix[row - 1, column + 1] == 1 || boardMatrix[row - 1, column + 1] == 3))
                        {
                            if (boardMatrix[row - 2, column + 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column + 2)));
                            }
                        }
                    }

                    // Bottom-left
                    if (row < NUMBER_OF_ROWS - 1 && column > 0)
                    {
                        if (boardMatrix[row + 1, column - 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column - 1)));
                        }
                        else if (row < NUMBER_OF_ROWS - 2 && column > 1 && (boardMatrix[row + 1, column - 1] == 1 || boardMatrix[row + 1, column - 1] == 3))
                        {
                            if (boardMatrix[row + 2, column - 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column - 2)));
                            }
                        }
                    }

                    // Bottom-right
                    if (row < NUMBER_OF_ROWS - 1 && column < NUMBER_OF_COLUMNS - 1)
                    {
                        if (boardMatrix[row + 1, column + 1] == 0)
                        {
                            possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column + 1)));
                        }
                        else if (row < NUMBER_OF_ROWS - 2 && column < NUMBER_OF_COLUMNS - 2 && (boardMatrix[row + 1, column + 1] == 1 || boardMatrix[row + 1, column + 1] == 3))
                        {
                            if (boardMatrix[row + 2, column + 2] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column + 2)));
                            }
                        }
                    }
                }
            }
            else
            {
                //White piece check
                if (boardMatrix[row, column] == 1)
                {
                    if (row < NUMBER_OF_ROWS - 1)
                    {
                        //Left side checking
                        if (column - 1 > -1)
                        {
                            if (boardMatrix[row + 1, column - 1] == 2 || boardMatrix[row + 1, column - 1] == 4)
                            {
                                if (column - 2 > -1 && row < NUMBER_OF_ROWS - 2 && boardMatrix[row + 2, column - 2] == 0)
                                {
                                    possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column - 2)));
                                }
                            }
                            else if (boardMatrix[row + 1, column - 1] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column - 1)));
                            }
                        }

                        //Right side checking
                        if (column + 1 < NUMBER_OF_COLUMNS)
                        {
                            if (boardMatrix[row + 1, column + 1] == 2 || boardMatrix[row + 1, column - 1] == 4)
                            {
                                if (column + 2 < NUMBER_OF_COLUMNS && row < NUMBER_OF_ROWS - 2 && boardMatrix[row + 2, column + 2] == 0)
                                {
                                    possbileMoves.Add(Tuple.Create((byte)(row + 2), (byte)(column + 2)));
                                }
                            }
                            else if (boardMatrix[row + 1, column + 1] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row + 1), (byte)(column + 1)));
                            }
                        }
                    }

                }

                //Red piece check
                if (boardMatrix[row, column] == 2)
                {
                    if (row > 0)
                    {
                        //Left side checking
                        if (column - 1 > -1)
                        {
                            if (boardMatrix[row - 1, column - 1] == 1 || boardMatrix[row - 1, column - 1] == 3)
                            {
                                if (column - 2 > -1 && row > 1 && boardMatrix[row - 2, column - 2] == 0)
                                {
                                    possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column - 2)));
                                }
                            }
                            else if (boardMatrix[row - 1, column - 1] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column - 1)));
                            }
                        }

                        //Right side checking
                        if (column + 1 < NUMBER_OF_COLUMNS)
                        {
                            if (boardMatrix[row - 1, column + 1] == 1 || boardMatrix[row - 1, column + 1] == 3)
                            {
                                if (column + 2 < NUMBER_OF_COLUMNS && row > 1 && boardMatrix[row - 2, column + 2] == 0)
                                {
                                    possbileMoves.Add(Tuple.Create((byte)(row - 2), (byte)(column + 2)));
                                }
                            }
                            else if (boardMatrix[row - 1, column + 1] == 0)
                            {
                                possbileMoves.Add(Tuple.Create((byte)(row - 1), (byte)(column + 1)));
                            }
                        }
                    }
                }
            }

            return possbileMoves;
        }
        private void InitMatrix(byte option)
        {
            if (option == 0)
            {
                SmallestBoardInit();
            }
            else if (option == 1)
            {
                MediumBoardInit();
            }
            else if (option == 2)
            {
                FullBoardInit();
            }
            else if (option == 4)
            {

            }
            else { throw new ArgumentException("Invalid board initialization option."); }
        }
        private void SmallestBoardInit()
        {
            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = 0;
                    }
                }
            }
        }

        private void MediumBoardInit()
        {
            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = 0;
                    }
                }
            }
        }
        private void FullBoardInit()
        {
            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 1;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 3 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 2;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = 0;
                    }
                }
            }
        }

        private void KingsBoardInit()
        {
            for (int rowIndex = 0; rowIndex < NUMBER_OF_ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < NUMBER_OF_COLUMNS; columnIndex++)
                {
                    if (rowIndex == 0 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 3;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 3 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = 4;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = 0;
                    }
                }
            }
        }
    }
}
