using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace educational_practice.ViewModels
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            execute = executeAction;
            canExecute = canExecuteAction;
        }

        public RelayCommand(Action<object> executeAction)
        {
            execute = executeAction;
            canExecute = null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}