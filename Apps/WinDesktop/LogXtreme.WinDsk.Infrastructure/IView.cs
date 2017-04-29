namespace LogXtreme.WinDsk.Infrastructure {
    public interface IView : IViewModelBase {

        IViewModel ViewModel { get; set; }
    }
}
