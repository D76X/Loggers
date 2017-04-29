namespace LogXtreme.WinDsk.Infrastructure {
    public interface IViewModel : IViewModelBase {

        IView View { get; set; }
    }
}
