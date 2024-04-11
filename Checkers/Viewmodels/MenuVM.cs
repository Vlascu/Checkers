using Checkers.Utils;
using Checkers.Viewmodels.Entities;
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
        private GameSettings gameSettings;
        private ICommand startNewGame;
        private ICommand loadNewGame;
        private ICommand openSettings;
        private ICommand openStatistics;
        private ICommand openAbout;

        public ICommand OpenAbout
        {
            get
            {
                if (openAbout == null)
                {
                    openAbout = new ParameterlessRelayCommand(GoToAbout, param => true);
                }
                return openAbout;
            }
            set
            {
                openAbout = value;
            }
        }

        public ICommand OpenStatistics
        {
            get
            {
                if (openStatistics == null)
                {
                    openStatistics = new ParameterlessRelayCommand(GoToStatistics, param => true);
                }
                return openStatistics;
            }
            set
            {
                openStatistics = value;
            }
        }

        public ICommand StartNewGame
        {
            get
            {
                if (startNewGame == null)
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
                if (loadNewGame == null)
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

        public ICommand OpenSettings
        {
            get
            {
                if (openSettings == null)
                {
                    openSettings = new ParameterlessRelayCommand(OpenSettingsWindow, param => true);
                }
                return openSettings;
            }
            set
            {
                openSettings = value;
            }
        }
        public MenuVM(Window menuWindow)
        {
            this.menuWindow = menuWindow;
            this.gameSettings = new GameSettings();
        }

        public MenuVM(Window menuWindow, GameSettings settings)
        {
            this.menuWindow = menuWindow;
            this.gameSettings = settings;

        }

        private void StartGame()
        {
            GameVM game = new GameVM(gameSettings);
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

        private void OpenSettingsWindow()
        {
            Settings settings = new Settings();
            menuWindow.Close();
            settings.ShowDialog();
        }

        private void GoToStatistics()
        {
            Statistics statistics = new Statistics();
            menuWindow.Close();
            statistics.ShowDialog();

        }

        private void GoToAbout()
        {
            About about = new About();
            menuWindow.Close();
            about.ShowDialog();
        }
    }
}
