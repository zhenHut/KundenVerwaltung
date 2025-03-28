using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KundenVerwaltung.Commands
{
   public class RelayCommand : ICommand
    {
        #region Constructor

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); ;
            _canExecute = canExecute;
        }

        #endregion

        #region Fields

        private readonly Action<object> _execute;
        private readonly Predicate<object>? _canExecute;

        #endregion

        #region Event

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Methods

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }

        #endregion

    }
}
