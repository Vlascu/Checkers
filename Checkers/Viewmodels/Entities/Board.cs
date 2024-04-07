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
        private static readonly byte WHITE_PIECE = (byte)PieceTypes.WHITE_PIECE;
        private static readonly byte WHITE_KING = (byte)PieceTypes.WHITE_KING;
        private static readonly byte RED_PIECE = (byte)PieceTypes.RED_PIECE;
        private static readonly byte RED_KING = (byte)PieceTypes.RED_KING;
        private static readonly byte EMPTY = (byte)PieceTypes.EMPTY;

        private byte[,] boardMatrix;
        private byte numberOfRedPieces;
        private byte numberOfWhitePieces;

        public Board(byte buildOption)
        {
            numberOfWhitePieces = 0;
            numberOfRedPieces = 0;
            boardMatrix = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
            InitMatrix(buildOption);
        }

        public List<Tuple<byte, byte>> GetAvailableMoves(byte row, byte column)
        {
            List<Tuple<byte, byte>> possibleMoves = new List<Tuple<byte, byte>>();

            byte pieceValue = boardMatrix[row, column];
            if (pieceValue == WHITE_KING || pieceValue == RED_KING)
            {
                if (pieceValue == WHITE_KING)
                    AddKingMoves(row, column, RED_PIECE, RED_KING, possibleMoves);
                else if (pieceValue == RED_KING)
                    AddKingMoves(row, column, WHITE_PIECE, WHITE_KING, possibleMoves);
            }
            else if (pieceValue == WHITE_PIECE || pieceValue == RED_PIECE)
            {
                byte opponentPieceValue = (byte)(pieceValue == WHITE_PIECE ? RED_PIECE : WHITE_PIECE);
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
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = EMPTY;
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
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = EMPTY;
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
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }
                    else if (rowIndex == 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = WHITE_PIECE;
                        numberOfWhitePieces++;
                    }

                    else if (rowIndex == NUMBER_OF_ROWS - 1 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 2 && columnIndex % 2 == 1)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }
                    else if (rowIndex == NUMBER_OF_ROWS - 3 && columnIndex % 2 == 0)
                    {
                        boardMatrix[rowIndex, columnIndex] = RED_PIECE;
                        numberOfRedPieces++;
                    }

                    else
                    {
                        boardMatrix[rowIndex, columnIndex] = EMPTY;
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
                    boardMatrix[rowDiag, colDiag] = EMPTY;
                }
            }

            if (rowDest == 0 || rowDest == NUMBER_OF_ROWS - 1)
            {
                if (movingPiece == WHITE_PIECE)
                {
                    boardMatrix[rowDest, columnDest] = WHITE_KING;
                }
                else if (movingPiece == RED_PIECE)
                {
                    boardMatrix[rowDest, columnDest] = RED_KING;
                }
            }
            else
            {
                boardMatrix[rowDest, columnDest] = movingPiece;
            }

            boardMatrix[rowSource, columnSource] = EMPTY;
        }

        public byte GetPieceType(byte row, byte column)
        {
            return boardMatrix[row, column];
        }
    }
}
