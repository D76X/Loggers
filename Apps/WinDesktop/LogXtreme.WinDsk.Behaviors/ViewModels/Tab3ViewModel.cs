using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class Tab3ViewModel : NotifyPropertyChangedBase {

        private string message = @"I have not been triggered yet...";
        private RelayCommand someCommand;
        private bool enabled;
        private int clickCounts;

        public Tab3ViewModel() {

            this.someCommand = new RelayCommand(
            this.ExecuteSomeCommand,
            this.CanExecuteSomeCommand);

            this.Enabled = true;
        }

        public string Message {
            get => this.message;
            set => this.SetProperty<string>(ref this.message, value);
        }

        public bool Enabled {

            get { return this.enabled; }

            set {
                this.SetProperty<bool>(ref this.enabled, value);
                this.someCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand SomeCommand =>
            this.someCommand;

        private void ExecuteSomeCommand() {

            this.Message = $"I was triggered on {DateTime.Now}";
        }

        private bool CanExecuteSomeCommand() => this.enabled;
    }
}
