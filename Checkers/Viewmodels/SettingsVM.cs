using Checkers.Utils;
using Checkers.Viewmodels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers.Views;
using System.Windows;

namespace Checkers.Viewmodels
{
    public class SettingsVM
    {
        private GameSettings settings;
        private Window window;

        private ICommand boardTypeSelect;
        private ICommand multipleMovesON;
        private ICommand multipleMovesOFF;
        private ICommand saveSettings;

        public SettingsVM(Window window)
        {
            settings = new GameSettings();
            this.window = window;
        }

        public ICommand SaveSettings
        {
            get
            {
                if (saveSettings == null)
                {
                    saveSettings = new ParameterlessRelayCommand(Save, param => true);
                }
                return saveSettings;
            }
            set { saveSettings = value; }
        }

        public ICommand BoardTypeSelect
        {
            get
            {
                if (boardTypeSelect == null)
                {
                    boardTypeSelect = new RelayCommand<int>(SaveBoardType, param => true);
                }
                return boardTypeSelect;
            }
            set
            {
                boardTypeSelect = value;
            }
        }

        public ICommand MultipleMovesON
        {
            get
            {
                if (multipleMovesON == null)
                {
                    multipleMovesON = new ParameterlessRelayCommand(MultipleOn, param => true);
                }
                return multipleMovesON;
            }
            set
            {
                multipleMovesON = value;
            }
        }

        public ICommand MultipleMovesOFF
        {
            get
            {
                if (multipleMovesOFF == null)
                {
                    multipleMovesOFF = new ParameterlessRelayCommand(MultipleOff, param => true);
                }
                return multipleMovesOFF;
            }
            set
            {
                multipleMovesOFF = value;
            }
        }

        private void SaveBoardType(int itemPos)
        {
            if (itemPos == 0)
            {
                settings.LoadBoard = BoardTypes.SmallestBoard();
            }
            else if (itemPos == 1)
            {
                settings.LoadBoard = BoardTypes.MediumBoard();
            }
            else if (itemPos == 2)
            {
                settings.LoadBoard = BoardTypes.FullBoard();
            }
            else if (itemPos == 3)
            {
                settings.LoadBoard = BoardTypes.KingsBoard();
            }
        }

        private void MultipleOn()
        {
            settings.MultipleMoves = true;
        }

        private void MultipleOff()
        {
            settings.MultipleMoves = false;
        }

        private void Save()
        {
            MessageBox.Show("Settings saved succesfully!");

            Menu menu = new Menu(settings);
            window.Close();
            menu.ShowDialog();
        }
    }
}
