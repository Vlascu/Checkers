using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.Utils
{
    class RelayCommand<T> : ICommand
    {
        private Action<T> commandTask;
        private Predicate<T> canExecuteTask;

        public RelayCommand(Action<T> workToDo, Predicate<T> canExecuteTask)
        {
            commandTask = workToDo;
            this.canExecuteTask = canExecuteTask;
        }

        public RelayCommand(Action<T> workToDo) : this(workToDo, DefaultCanExecute)
        {
            commandTask = workToDo;
          
        }
        private static bool DefaultCanExecute(T parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return canExecuteTask != null && canExecuteTask((T)parameter);
        }

        public void Execute(object parameter)
        {
          
           commandTask((T)parameter);

        }
    }
}
