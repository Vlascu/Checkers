﻿
using Checkers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema1_Dictionar;
using Checkers.Viewmodels.Entities;
using Checkers.Views;
using System.Windows;

namespace Checkers.Viewmodels
{
    public class LoadVM
    {
        private List<byte[,]> boards;
        private List<Tuple<byte[,], string>> listItems;
        private Window currentWindow;

        private ICommand startGame;

        public List<Tuple<byte[,], string>> ListItems { get { return listItems; } }

        public ICommand StartGame
        {
            get
            {
                if (startGame == null)
                {
                    startGame = new RelayCommand<string>(StartLoadedGame, param => true);
                }
                return startGame;
            }
            set
            {
                startGame = value;
            }
        }

        public LoadVM(Window window)
        {
            this.currentWindow = window;
            listItems = new List<Tuple<byte[,], string>>();
            boards = JsonPersitence.LoadFromJson<byte[,]>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Checkers\Checkers\Model\games.json");
            InitListItems();
        }

        private void InitListItems()
        {
            int counter = 1;
            if (boards != null)
            {
                foreach (var board in boards)
                {
                    listItems.Add(new Tuple<byte[,], string>(board, "Game " + counter));
                    counter++;
                }
            }
        }

        private void StartLoadedGame(string gameName)
        {
            foreach(var games in listItems)
            {
                if(games.Item2 == gameName)
                {
                    GameSettings settings = new GameSettings();
                    settings.LoadBoard = games.Item1;

                    GameVM gameVM = new GameVM(settings);
                    GameBoard board = new GameBoard(gameVM);
                    currentWindow.Close();

                    board.ShowDialog();
                }
            }
        }
    }
}
