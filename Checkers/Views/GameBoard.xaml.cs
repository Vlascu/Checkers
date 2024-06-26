﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Checkers.Viewmodels;

namespace Checkers.Views
{
    /// <summary>
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        private GameVM gameVM;
        public GameBoard(GameVM gameVM)
        {
            InitializeComponent();
            this.gameVM = gameVM;
            this.gameVM.CurrentWindow = this;
            DataContext = gameVM;
        }

        public GameBoard()
        {
            InitializeComponent();
            DataContext = new GameVM();
        }

    }
}
