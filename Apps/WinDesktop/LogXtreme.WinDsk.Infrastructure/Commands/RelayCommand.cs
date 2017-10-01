using System;
using System.Windows.Input;

namespace LogXtreme.WinDsk.Infrastructure.Commands {

    /// <summary> 
    /// Standard implementation of the ICommand. This is additional to the implementations 
    /// already available in the Prims library as DelegateCommand and the DelegateCommand<T>.
    /// Genarally DelegateCommand and the DelegateCommand<T> from the Prism library should 
    /// be used instead of RelayCommand and RelayCommand<T> but there are cases where these 
    /// additional implementations are useful. Fr example in all demo applications that must
    /// reference LogXtreme.WinDsk.Infrastructure and support Commands but that do not need 
    /// any of the assets from Prims.
    /// 
    /// Refs
    /// 
    /// Command
    /// https://msdn.microsoft.com/en-us/library/gg405484(v=pandp.40).aspx
    /// https://stackoverflow.com/questions/1468791/wpf-icommand-mvvm-implementation
    /// 
    /// Commad implementation with CommandManager
    /// https://stackoverflow.com/questions/2763630/how-does-commandmanager-requerysuggested-work
    /// 
    /// </summary>
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

        #region ICommand
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
