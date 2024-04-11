using Checkers.Viewmodels;
using Checkers.Viewmodels.Entities;
using System;
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

namespace Checkers.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            DataContext = new MenuVM(this);
        }

        public Menu(MenuVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public Menu(GameSettings settings)
        {
            InitializeComponent();
            DataContext = new MenuVM(this, settings);
        }
    }
}
