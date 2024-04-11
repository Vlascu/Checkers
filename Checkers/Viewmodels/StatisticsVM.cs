using Checkers.Utils;
using Checkers.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema1_Dictionar;

namespace Checkers.Viewmodels
{
    public class StatisticsVM
    {
        private ICommand goBack;
        private Window window;
        public List<Tuple<string, int>> Winners { get; set; }
        public string Reds {  get; set; }
        public string Whites {  get; set; }

        public ICommand GoBack
        {
            get
            {
                if(goBack == null)
                {
                    goBack = new ParameterlessRelayCommand(Back, param => true);
                }
                return goBack;
            }
            set
            {
                goBack = value;
            }
        }

        public StatisticsVM(Window window)
        {
            Reds = "Red wins: 0";
            Whites = "White wins: 0";

            this.window = window;
            Winners = JsonPersitence.LoadFromJson<Tuple<string, int>>(@"C:\\Users\\Vlascu\\Desktop\\Cursuri UNITBV\\ANUL 2\\Sem 2\\MAP\\Checkers\\Checkers\\Model\\winners.json");
            CalculateNumberOfWinners();
        }

        private void Back()
        {
            Menu menu = new Menu();
            window.Close();
            menu.ShowDialog();
        }

        private void CalculateNumberOfWinners()
        {
            int whiteCounter = 0;
            int redCounter = 0;

            foreach(var winner in Winners)
            {
                if (winner.Item1 == "White")
                {
                    whiteCounter++;
                } else if (winner.Item1 == "Red")
                {
                    redCounter++;
                }
            }

            Reds = "Red wins: " + redCounter;
            Whites = "White wins: " + whiteCounter;
        }
    }
}
