using Checkers.Utils;
using Checkers.Viewmodels.Entities;
using Checkers.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Tema1_Dictionar;


namespace Checkers.Viewmodels
{
    public class GameVM : INotifyPropertyChanged
    {
        private static byte defaultOption = 2;
        public ObservableCollection<Cell> GridMatrix { get; set; }
        private Board board;
        private bool isBoardColored;
        private bool multipleMoves;
        private bool isRedMoving;
        private byte rowSourceIndex;
        private byte colSourceIndex;
        private string movingPlayer;
        private Window currentWindow;

        private ICommand getMovePositions;
        private ICommand undoMovePositions;
        private ICommand movePieceCommand;
        private ICommand saveGame;
        private ICommand exitGame;

        public bool CanGetPositions { get; set; } = true;
        public bool CanExecuteUndo { get; set; } = false;
        public bool CanMovePiece { get; set; } = false;
        public byte DefaultOption { get; set; }
        public bool IsRedMoving
        {
            get
            {
                return isRedMoving;
            }
            set
            {
                isRedMoving = value;
            }
        }

        public string MovingPlayer
        {
            get { return movingPlayer; }
            set
            {
                if (movingPlayer != value)
                {
                    movingPlayer = value;
                    OnPropertyChanged(nameof(MovingPlayer));
                }
            }
        }

        public Window CurrentWindow
        {
            get { return currentWindow; }
            set { currentWindow = value; }
        }

        public bool MultipleMoves
        {
            set
            {
                multipleMoves = value;
            }
        }
        public ICommand ExitGame
        {
            get
            {
                if (exitGame == null)
                {
                    exitGame = new ParameterlessRelayCommand(GoToMenu, param => true);
                }
                return exitGame;
            }
            set { exitGame = value; }
        }
        public ICommand SaveGame
        {
            get
            {
                if (saveGame == null)
                {
                    saveGame = new ParameterlessRelayCommand(SaveCurrentGame, param => true);
                }
                return saveGame;
            }
            set { saveGame = value; }
        }
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
            isRedMoving = true;
            multipleMoves = false;
            SetLabelWhoMoves();

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

        public GameVM(GameSettings settings)
        {
            GridMatrix = new ObservableCollection<Cell>();
            isBoardColored = false;
            isRedMoving = true;
            multipleMoves = settings.MultipleMoves;
            SetLabelWhoMoves();

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
            InitBoardAndGrid(settings.LoadBoard);
        }

        private void InitBoardAndGrid(byte option)
        {
            board = new Board(option);

            RenderBoard();
            isBoardColored = true;
        }

        private void InitBoardAndGrid(byte[,] loadedBoard)
        {
            board = new Board(loadedBoard);

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
                    cell.Image = PieceBuilder.GetPieceImage(6);
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

        private void GetAvailablePositions(Cell cell)
        {
            if ((isRedMoving &&
                (PieceBuilder.AreBitmapsEqual((BitmapImage)cell.Image, PieceBuilder.GetPieceImage((byte)PieceTypes.RED_PIECE))
                || PieceBuilder.AreBitmapsEqual((BitmapImage)cell.Image, PieceBuilder.GetPieceImage((byte)PieceTypes.RED_KING))))
                || (!isRedMoving && ((PieceBuilder.AreBitmapsEqual((BitmapImage)cell.Image, PieceBuilder.GetPieceImage((byte)PieceTypes.WHITE_PIECE))
                || PieceBuilder.AreBitmapsEqual((BitmapImage)cell.Image, PieceBuilder.GetPieceImage((byte)PieceTypes.WHITE_KING))))))
            {
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

            }
        }

        public void SetLabelWhoMoves()
        {
            if (isRedMoving)
            {
                MovingPlayer = "Red player is moving";
            }
            else
            {
                MovingPlayer = "White player is moving";
            }
        }

        private void MovePiece(Cell cell)
        {
            byte rowIndex = cell.RowIndex;
            byte columnIndex = cell.ColumnIndex;

            byte beforeNumberOfWhites = board.NumberOfWhitePieces;
            byte beforeNumberOfReds = board.NumberOfRedPieces;

            if (cell.Background.Color == Color.FromRgb(44, 171, 49))
            {
                board.MovePiece(rowSourceIndex, colSourceIndex, rowIndex, columnIndex);
                UndoPositions();
                RenderBoard();
            }

            if (board.NumberOfRedPieces == 0)
            {
                MessageBox.Show("Congrats, white player! You've won!");
                SaveUserInfo("White");
                GoToMenu();
            }

            if (board.NumberOfWhitePieces == 0)
            {
                MessageBox.Show("Congrats, red player! You've won!");
                SaveUserInfo("Red");
                GoToMenu();
            }

            CanMovePiece = false;

            if (multipleMoves)
            {
                bool canTakeOneMore = false;

                if (beforeNumberOfReds != board.NumberOfRedPieces || beforeNumberOfWhites != board.NumberOfWhitePieces)
                {
                    var poses = board.GetAvailableMoves(rowIndex, columnIndex);
                    canTakeOneMore = CheckCanTakeOneMore(poses, rowIndex, columnIndex);
                }

                if (!canTakeOneMore)
                {
                    isRedMoving = !isRedMoving;
                }

            }
            else
            {
                isRedMoving = !isRedMoving;
            }

            SetLabelWhoMoves();

        }

        private bool CheckCanTakeOneMore(List<Tuple<byte, byte>> poses, byte rowIndex, byte columnIndex)
        {
            foreach (var item in poses)
            {
                if (Math.Abs(item.Item1 - rowIndex) == 2 && Math.Abs(item.Item2 - columnIndex) == 2)
                {
                    return true;
                }
            }
            return false;
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

        private void SaveCurrentGame()
        {
            try
            {
                List<Tuple<byte[,], bool>> allGames = JsonPersitence.LoadFromJson<Tuple<byte[,], bool>>(@"C:\\Users\\Vlascu\\Desktop\\Cursuri UNITBV\\ANUL 2\\Sem 2\\MAP\\Checkers\\Checkers\\Model\\games.json");

                if (allGames != null)
                {
                    bool matrixEquals = allGames.Any(pair => AreMatricesEqual(pair.Item1, board.BoardMatrix));

                    if (matrixEquals)
                    {
                        MessageBox.Show("Can't save an already existing game configuration");
                        return;
                    }
                }

                JsonPersitence.SaveToJson(new Tuple<byte[,], bool>(board.BoardMatrix, this.isRedMoving), @"C:\\Users\\Vlascu\\Desktop\\Cursuri UNITBV\\ANUL 2\\Sem 2\\MAP\\Checkers\\Checkers\\Model\\games.json");

                MessageBox.Show("Current game saved succesfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void SaveUserInfo(string user)
        {
            byte numberOfPiecesLeft = 0;

            if (user == "White")
            {
                numberOfPiecesLeft = board.NumberOfWhitePieces;
            }
            else if (user == "Red")
            {
                numberOfPiecesLeft = board.NumberOfRedPieces;
            }

            try
            {
                JsonPersitence.SaveToJson(new Tuple<string, byte>(user, numberOfPiecesLeft), @"C:\\Users\\Vlascu\\Desktop\\Cursuri UNITBV\\ANUL 2\\Sem 2\\MAP\\Checkers\\Checkers\\Model\\winners.json");
                MessageBox.Show("User info saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void GoToMenu()
        {
            Menu menu = new Menu();
            currentWindow.Close();
            menu.ShowDialog();
        }

        private bool AreMatricesEqual(byte[,] matrix1, byte[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) ||
                matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

