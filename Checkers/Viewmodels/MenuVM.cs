using Checkers.Utils;
using Checkers.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Checkers.Viewmodels
{
    public class MenuVM
    {
        private readonly Window menuWindow;
        private ICommand startNewGame;
        private ICommand loadNewGame;

        public ICommand StartNewGame
        {
            get
            {
                if(startNewGame == null)
                {
                    startNewGame = new ParameterlessRelayCommand(StartGame, param => true);
                }
                return startNewGame;
            }
            set
            {
                startNewGame = value;
            }
        }

        public ICommand LoadNewGame
        {
            get
            {
                if(loadNewGame == null)
                {
                    loadNewGame = new ParameterlessRelayCommand(LoadGame, param => true);

                }
                return loadNewGame;
            }
            set
            {
                loadNewGame = value;
            }
        }

        public MenuVM(Window menuWindow)
        {
            this.menuWindow = menuWindow;
        }

        private void StartGame()
        {
            GameVM game = new GameVM();
            GameBoard board = new GameBoard(game);
            menuWindow.Close();
            board.ShowDialog();
        }

        private void LoadGame()
        {
            Load load = new Load();
            menuWindow.Close();
            load.ShowDialog();
        }

    }
}
