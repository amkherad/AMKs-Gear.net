using System;
using System.Windows.Input;

namespace AMKsGear.Core.Automation.Support
{
    public class AutoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> ActionParam { get; }
        public Action Action { get; }
        public Func<object, bool> CanExecuteCallbackParam { get; }
        public Func<bool> CanExecuteCallback { get; }

        public AutoCommand(Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            ActionParam = execute;
        }
        public AutoCommand(Action execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            Action = execute;
        }
        public AutoCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            ActionParam = execute;
            CanExecuteCallbackParam = canExecute;
        }
        public AutoCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            Action = execute;
            CanExecuteCallback = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var canExecuteParam = CanExecuteCallbackParam;
            if (canExecuteParam != null)
            {
                return canExecuteParam(parameter);
            }

            var canExecute = CanExecuteCallback;
            if (canExecute != null)
            {
                return canExecute();
            }

            return true;
        }
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public void Execute(object parameter)
        {
            var canExecuteParam = CanExecuteCallbackParam;
            if (canExecuteParam != null)
            {
                canExecuteParam(parameter);
                return;
            }

            var canExecute = CanExecuteCallback;
            if (canExecute != null)
            {
                canExecute();
                return;
            }
        }
    }
}