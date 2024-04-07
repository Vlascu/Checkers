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
            List<Tuple<byte, byte>> possibleMoves = new List<Tuple<byte, byte>>();

            byte pieceValue = boardMatrix[row, column];
            if (pieceValue == 3 || pieceValue == 4)
            {
                if (pieceValue == 3)
                    AddKingMoves(row, column, 2, 4, possibleMoves);
                else if (pieceValue == 4)
                    AddKingMoves(row, column, 1, 3, possibleMoves);
            }
            else if (pieceValue == 1 || pieceValue == 2)
            {
                byte opponentPieceValue = (byte)(pieceValue == 1 ? 2 : 1);
                AddRegularPieceMoves(row, column, opponentPieceValue, possibleMoves);
            }

            return possibleMoves;
        }
        
        private void AddKingMoves(byte row, byte column, byte opponentPiece, byte opponentKing, List<Tuple<byte, byte>> possibleMoves)
        {
            AddDiagonalMove(row, column, -1, -1, opponentPiece, opponentKing, possibleMoves);
            AddDiagonalMove(row, column, -1, 1, opponentPiece, opponentKing, possibleMoves);
            AddDiagonalMove(row, column, 1, -1, opponentPiece, opponentKing, possibleMoves);
            AddDiagonalMove(row, column, 1, 1, opponentPiece, opponentKing, possibleMoves);
        }

        private void AddRegularPieceMoves(byte row, byte column, byte opponentPiece, List<Tuple<byte, byte>> possibleMoves)
        {
            AddDiagonalMove(row, column, 1, -1, opponentPiece, 0, possibleMoves);
            AddDiagonalMove(row, column, 1, 1, opponentPiece, 0, possibleMoves);
        }

        private void AddDiagonalMove(byte row, byte column, int rowDirection, int columnDirection, byte opponentPiece, byte opponentKing, List<Tuple<byte, byte>> possibleMoves)
        {
            int newRow = row + rowDirection;
            int newColumn = column + columnDirection;

            if (IsValidCell(newRow, newColumn) && (boardMatrix[newRow, newColumn] == 0))
            {
                possibleMoves.Add(Tuple.Create((byte)newRow, (byte)newColumn));
            }
            else if (IsValidCell(newRow, newColumn) && (boardMatrix[newRow, newColumn] == opponentPiece || boardMatrix[newRow, newColumn] == opponentKing))
            {
                newRow += rowDirection;
                newColumn += columnDirection;

                if (IsValidCell(newRow, newColumn) && (boardMatrix[newRow, newColumn] == 0))
                {
                    possibleMoves.Add(Tuple.Create((byte)newRow, (byte)newColumn));
                }
            }
        }

        private bool IsValidCell(int row, int column)
        {
            return row >= 0 && row < NUMBER_OF_ROWS && column >= 0 && column < NUMBER_OF_COLUMNS;
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
                KingsBoardInit();
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

        public void MovePiece(byte rowSource, byte columnSource, byte rowDest, byte columnDest)
        {
            byte movingPiece = boardMatrix[rowSource, columnSource];


            if (Math.Abs(rowDest - rowSource) == 2 && Math.Abs(columnDest - columnSource) == 2)
            {

                byte rowDiag = (byte)((rowSource + rowDest) / 2);
                byte colDiag = (byte)((columnSource + columnDest) / 2);

                if (boardMatrix[rowDiag, colDiag] != 0)
                {
                    boardMatrix[rowDiag, colDiag] = 0;
                }
            }

            boardMatrix[rowDest, columnDest] = movingPiece;

            boardMatrix[rowSource, columnSource] = 0;
        }

        public byte GetPieceType(byte row, byte column)
        {
            return boardMatrix[row, column];
        }
    }
}
