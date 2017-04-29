using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;

namespace ModuleC.ViewModels {
    public class ViewBViewModel : IViewBViewModel {
        public IView View { get; set; }

        public ViewBViewModel(IViewB view) {
            this.View = view;
            this.View.ViewModel = this;
        }
    }
}
