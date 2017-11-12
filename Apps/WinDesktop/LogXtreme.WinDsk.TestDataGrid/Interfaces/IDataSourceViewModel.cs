using System;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface IDataSourceViewModel : IDisposable {       

        ICommand CommandStartReadingData { get; }

        ICommand CommandStopReadingData { get; }

        bool ReadingData { get; }
    }
}
