using LogXtreme.WinDsk.Infrastructure.Wpf;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    public class Tab2ViewModel : NotifyPropertyChangedBase {

        private string message = @"Message from View Model";

        public Tab2ViewModel() { }        

        public string Message {
            get => this.message;
            set => this.SetProperty<string>(ref this.message, value);
        }
    }
}
