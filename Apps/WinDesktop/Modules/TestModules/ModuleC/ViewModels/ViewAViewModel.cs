using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;

namespace ModuleC.ViewModels {
    public class ViewAViewModel : IViewAViewModel {  

        public IView View { get; set; }

        public ViewAViewModel(IViewA view) {
            this.View = view;
            this.View.ViewModel = this;
        }
    }
}
