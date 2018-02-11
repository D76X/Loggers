using LogXtreme.WinDsk.Infrastructure.Wpf;
using System.Collections;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    public class MainWindowViewModel : NotifyPropertyChangedBase {

        ArrayList viewModels = new ArrayList();

        public MainWindowViewModel() {            

            // Tab1
            this.viewModels.Add(new Tab1ViewModel());
        }

        public Tab1ViewModel Tab1ViewModel =>
            (Tab1ViewModel)this.viewModels[0];
    }
}
