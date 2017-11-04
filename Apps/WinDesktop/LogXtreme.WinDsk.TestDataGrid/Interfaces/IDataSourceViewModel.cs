using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {
    public interface IDataSourceViewModel {

        ICommand CommandReadNext { get; }

        ICommand CommandStartReading { get; }

        ICommand CommandStopReading { get; }
    }
}
