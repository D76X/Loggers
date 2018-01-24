using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace LogXtreme.WinDsk.Behaviors.ViewModels {

    public class MainWindowViewModel : INotifyPropertyChanged {

        private Timer timer = new Timer(5000);

        public MainWindowViewModel() {

            // some child view model
            this.ChildViewModel = new object();

            timer.Elapsed += (s, e) => NotificationMessage = $"Time is {DateTime.Now}";
            timer.Start();
        }

        public object ChildViewModel { get; set; }


        private string notificationMessage;

        public string NotificationMessage {

            get => notificationMessage;

            set {

                if (value != notificationMessage) {
                    notificationMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This handler is used to test the HanldeViewLoadedEventBehaviour
        /// and has no importance in the Data Grid test.
        /// </summary>
        public void OnViewLoadedHandler() {

            // do some data loading here i.e. async...
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion  

    }
}
