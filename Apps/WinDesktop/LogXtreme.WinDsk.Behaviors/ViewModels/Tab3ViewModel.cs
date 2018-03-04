using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class Tab3ViewModel : NotifyPropertyChangedBase {

        private string message = @"I have not been triggered yet...";
        private RelayCommand<object> notifyCommand;
        private bool enabled;
        private int clickCounts;

        public Tab3ViewModel() {

            this.notifyCommand = new RelayCommand<object>(
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
                this.notifyCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand NotifyCommand =>
            this.notifyCommand;

        private void ExecuteSomeCommand(object param) {

            string triggeredOn = $" {DateTime.Now}";
            string triggeredAt = $"x=?, y=?";

            if (param is Point) {

                Point p = (Point) param;

                triggeredAt = $"x={p.X}, y={p.Y}";
            }


            this.Message = $"Triggered on {triggeredOn} at {triggeredAt}";
        }

        private bool CanExecuteSomeCommand(object param) => this.enabled;
    }
}
