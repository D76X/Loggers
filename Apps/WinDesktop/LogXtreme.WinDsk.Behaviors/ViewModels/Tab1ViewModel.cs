using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    public class Tab1ViewModel : NotifyPropertyChangedBase {

        private Timer timer = new Timer(5000);
        private bool isComponentLoaded = false;
        private bool isDataLodaed = false;
        private bool isDataLoading = false;

        public Tab1ViewModel() {

            IsComponentLoaded = false;
            timer.Elapsed += (s, e) => NotificationMessage = $"Time is {DateTime.Now}";
            timer.Start();
        }

        private string notificationMessage;

        public string NotificationMessage {
            get => notificationMessage;
            set => SetProperty<string>(ref notificationMessage, value);
        }

        public bool IsComponentLoaded {

            get => isComponentLoaded;
            private set => SetProperty<bool>(ref isComponentLoaded, value);
        }

        public bool IsDataLoading {

            get => isDataLoading;
            private set => SetProperty<bool>(ref isDataLoading, value);
        }

        public bool IsDataLoaded {

            get => isDataLodaed;
            private set => SetProperty<bool>(ref isDataLodaed, value);
        }

        /// <summary>
        /// This handler is used to test the HanldeViewLoadedEventBehaviour
        /// and has no importance in the Data Grid test.
        /// </summary>
        public async void OnViewLoadedHandler() {

            IsComponentLoaded = true;

            // do some data loading here i.e. async...
            IsDataLoading = await PrepareDataLoading();

            IsDataLoaded = await LoadData();
        }

        private async Task<bool> PrepareDataLoading() {

            await Task<bool>.Delay(2000);

            return true;
        }

        private async Task<bool> LoadData() {

            await Task<bool>.Delay(2000);

            return true;
        }
    }
}
