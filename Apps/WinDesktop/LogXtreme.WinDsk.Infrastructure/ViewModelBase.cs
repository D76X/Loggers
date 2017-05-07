using System;
using Prism.Mvvm;
using Prism.Regions;

namespace LogXtreme.WinDsk.Infrastructure {

    public class ViewModelBase : BindableBase, IViewModelBase, INavigationAware {

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext) {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext) {
        }
    }
}
