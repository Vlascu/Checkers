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
    public class AboutVM
    {
        private Window window;
        private ICommand exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new ParameterlessRelayCommand(ExitAbout, param => true);
                }
                return exitCommand;
            }
            set
            {
                exitCommand = value;
            }
        }

        public AboutVM(Window window)
        {
            this.window = window; 
        }

        private void ExitAbout()
        {
            Menu menu = new Menu();
            window.Close();
            menu.ShowDialog();
        }
    }
}
