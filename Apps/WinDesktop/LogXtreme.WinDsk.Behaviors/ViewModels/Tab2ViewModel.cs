using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Wpf;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class Tab2ViewModel : NotifyPropertyChangedBase {

        private string message = @"Message from View Model";
        private RelayCommand sendMessageCommand;
        private bool enabled;
        private int clickCounts;

        public Tab2ViewModel() {

            this.sendMessageCommand = new RelayCommand(
            this.ExecuteSendMessage,
            this.CanExecuteSendMessage);

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
                this.sendMessageCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand SendMessageCommand =>
            this.sendMessageCommand;

        private void ExecuteSendMessage() {

            this.Message = $"Clicks = {++clickCounts}";
        }

        private bool CanExecuteSendMessage() => this.enabled;
    }
}
