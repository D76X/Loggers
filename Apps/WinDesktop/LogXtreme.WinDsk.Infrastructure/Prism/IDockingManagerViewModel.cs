using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    public interface IDockingManagerViewModel {

        object ActiveContent { get; }

        void SetDockingManagerSyncBehavior(DockingManagerSyncBehavior syncBehavior);

        ReadOnlyObservableCollection<object> Anchorables { get; }

        ReadOnlyObservableCollection<object> Documents { get; }
    }
}
