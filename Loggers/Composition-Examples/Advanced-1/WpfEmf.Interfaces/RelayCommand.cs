using System;
using System.Windows.Input;

namespace WpfEmf.Interfaces {
    public class RelayCommand : ICommand {

        private Action targetExecute;
        private Func<bool> targetCanExecute;

        public RelayCommand(Action executeMethod) {
            targetExecute = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecute) {
            targetExecute = executeMethod;
            targetCanExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand

        /// <summary>
        /// Should use weak reference - see Prism
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };

        bool ICommand.CanExecute(object parameter) {

            if (targetCanExecute != null) {
                return targetCanExecute();
            }

            if (targetExecute != null) {
                return true;
            }

            return false;
        }

        void ICommand.Execute(object parameter) {

            targetExecute?.Invoke();
        }
        #endregion
    }


    /// <summary>
    /// http://stackoverflow.com/questions/6273002/generic-type-safe-icommand-implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand {

        private Action<T> targetExecute;
        private Predicate<T> targetCanExecute;

        public RelayCommand(Action<T> executeMethod) {
            targetExecute = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Predicate<T> canExecute) {
            targetExecute = executeMethod;
            targetCanExecute = canExecute;
        }

        #region
        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter) {

            if (targetCanExecute != null) {
                return targetCanExecute((T)parameter);
            }

            if (targetExecute != null) {
                return true;
            }

            return false;
        }

        public void Execute(object parameter) {

            targetExecute?.Invoke((T)parameter);
        }
        #endregion
    }
}
