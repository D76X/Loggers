using LogXtreme.WinDsk.Infrastructure.Wpf;
using System.Collections;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    public class MainWindowViewModel : NotifyPropertyChangedBase {

        ArrayList viewModels = new ArrayList();

        public MainWindowViewModel() {            

            // Tab1
            this.viewModels.Add(new Tab1ViewModel());

            // Tab2
            this.viewModels.Add(new Tab2ViewModel());

            // Tab3
            this.viewModels.Add(new Tab3ViewModel());
        }

        public Tab1ViewModel Tab1ViewModel =>
            (Tab1ViewModel)this.viewModels[0];

        public Tab2ViewModel Tab2ViewModel =>
            (Tab2ViewModel)this.viewModels[1];

        public Tab3ViewModel Tab3ViewModel =>
            (Tab3ViewModel)this.viewModels[2];
    }
}
