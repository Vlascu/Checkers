using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.Utils
{
    class ParameterlessRelayCommand : ICommand
    {
        private Action commandTask;
        private Predicate<object> canExecuteTask;

        public ParameterlessRelayCommand(Action workToDo, Predicate<object> canExecuteTask)
        {
            commandTask = workToDo;
            this.canExecuteTask = canExecuteTask;
        }

        public ParameterlessRelayCommand(Action workToDo) : this(workToDo, DefaultCanExecute)
        {
            commandTask = workToDo;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecuteTask != null && canExecuteTask(parameter);
        }

        public void Execute(object parameter)
        {
            commandTask();
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return false;
        }
    }
}
