using Checkers.Utils;
using Checkers.Viewmodels.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;


namespace Checkers.Viewmodels
{
    public class GameVM
    {
        public static byte defaultOption = 2;
        public ObservableCollection<Cell> GridMatrix { get; set; }
        private Board board;
        private bool isBoardColored;
        private byte rowSourceIndex;
        private byte colSourceIndex;

        private ICommand getMovePositions;
        private ICommand undoMovePositions;
        private ICommand movePieceCommand;

        public bool CanGetPositions { get; set; } = true;
        public bool CanExecuteUndo { get; set; } = false;
        public bool CanMovePiece { get; set; } = false;

        public ICommand MoveOrGetPositions
        {
            get
            {
                return new RelayCommand<Cell>(param =>
                {
                    if (CanGetPositions)
                    {
                        GetAvailablePositions(param);
                    }
                    else
                    {
                        MovePiece(param);
                    }
                });
            }
            set { }
        }

        public ICommand GetMovePositions
        {
            get
            {
                if (getMovePositions == null)
                {
                    getMovePositions = new RelayCommand<Cell>(GetAvailablePositions, param => CanGetPositions);
                }
                return getMovePositions;
            }
            set { getMovePositions = value; }
        }

        public ICommand UndoMovePositions
        {
            get
            {
                if (undoMovePositions == null)
                {
                    undoMovePositions = new ParameterlessRelayCommand(UndoPositions, param => CanExecuteUndo);
                }
                return undoMovePositions;
            }
            set { undoMovePositions = value; }
        }

        public ICommand MovePieceCommand
        {
            get
            {
                if (movePieceCommand == null)
                {
                    movePieceCommand = new RelayCommand<Cell>(MovePiece, param => CanMovePiece);
                }
                return movePieceCommand;
            }
            set { movePieceCommand = value; }
        }

        public GameVM()
        {
            GridMatrix = new ObservableCollection<Cell>();
            isBoardColored = false;

            for (byte rowIndex = 0; rowIndex < 8; rowIndex++)
            {
                for (byte columnIndex = 0; columnIndex < 8; columnIndex++)
                {
                    Cell cell = new Cell
                    {
                        RowIndex = rowIndex,
                        ColumnIndex = columnIndex
                    };
                    GridMatrix.Add(cell);
                }
            }
            InitBoardAndGrid(defaultOption);
        }
        public GameVM(byte option)
        {
            GridMatrix = new ObservableCollection<Cell>();
            isBoardColored = false;


            for (byte rowIndex = 0; rowIndex < 8; rowIndex++)
            {
                for (byte columnIndex = 0; columnIndex < 8; columnIndex++)
                {
                    Cell cell = new Cell
                    {
                        RowIndex = rowIndex,
                        ColumnIndex = columnIndex
                    };
                    GridMatrix.Add(cell);
                }
            }
            InitBoardAndGrid(option);
        }

        private void InitBoardAndGrid(byte option)
        {
            board = new Board(option);

            RenderBoard();
            isBoardColored = true;
        }

        private void RenderBoard()
        {
            foreach (Cell cell in GridMatrix)
            {
                byte pieceType = board.GetPieceType(cell.RowIndex, cell.ColumnIndex);

                if (pieceType == (byte)PieceTypes.EMPTY)
                {
                    cell.IsEnabled = false;
                    cell.Image = null;
                }
                else
                {
                    cell.IsEnabled = true;
                    cell.Image = PieceBuilder.GetPieceImage(pieceType);
                }

                if (!isBoardColored)
                {
                    SetBackgroundColor(cell);
                }

            }
        }
        private void SetBackgroundColor(Cell cell)
        {
            if (cell.RowIndex % 2 == 0)
            {
                cell.Background = cell.ColumnIndex % 2 == 0 ?
                    new SolidColorBrush(Color.FromArgb(255, 252, 215, 139)) :
                    new SolidColorBrush(Color.FromArgb(255, 201, 144, 38));
            }
            else
            {
                cell.Background = cell.ColumnIndex % 2 == 1 ?
                    new SolidColorBrush(Color.FromArgb(255, 252, 215, 139)) :
                    new SolidColorBrush(Color.FromArgb(255, 201, 144, 38));
            }
        }

        public byte DefaultOption
        {
            set { defaultOption = value; }
        }


        private void GetAvailablePositions(Cell cell)
        {
            byte rowIndex = cell.RowIndex;
            byte columnIndex = cell.ColumnIndex;

            rowSourceIndex = rowIndex;
            colSourceIndex = columnIndex;

            var availablePositions = board.GetAvailableMoves(rowIndex, columnIndex);

            if (availablePositions.Count != 0)
            {
                foreach (var move in availablePositions)
                {
                    foreach (Cell cellItem in GridMatrix.Where(x => x.RowIndex == move.Item1 && x.ColumnIndex == move.Item2))
                    {
                        cellItem.Background = new SolidColorBrush(Color.FromRgb(44, 171, 49));
                        cellItem.IsEnabled = true;
                        cellItem.Image = PieceBuilder.GetPieceImage(5);

                        CanGetPositions = false;
                        CanExecuteUndo = true;
                        CanMovePiece = true;
                    }
                }
            }
        }

        private void MovePiece(Cell cell)
        {
            byte rowIndex = cell.RowIndex;
            byte columnIndex = cell.ColumnIndex;

            if (cell.Background.Color == Color.FromRgb(44, 171, 49))
            {
                board.MovePiece(rowSourceIndex, colSourceIndex, rowIndex, columnIndex);
                UndoPositions();
                RenderBoard();
            }

            CanMovePiece = false;
        }
        private void UndoPositions()
        {

            foreach (Cell cell in GridMatrix)
            {
                if (cell.Background.Color == Color.FromRgb(44, 171, 49))
                {
                    if (cell.RowIndex % 2 == 0)
                    {
                        cell.Background = cell.ColumnIndex % 2 == 0 ?
                            new SolidColorBrush(Color.FromArgb(255, 252, 215, 139)) :
                            new SolidColorBrush(Color.FromArgb(255, 201, 144, 38));
                    }
                    else
                    {
                        cell.Background = cell.ColumnIndex % 2 == 1 ?
                            new SolidColorBrush(Color.FromArgb(255, 252, 215, 139)) :
                            new SolidColorBrush(Color.FromArgb(255, 201, 144, 38));
                    }
                    cell.IsEnabled = false;
                    cell.Image = null;
                }
                
            }

            CanGetPositions = true;
        }

    }
}
