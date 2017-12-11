
using LogXtreme.WinDsk.Infrastructure.Models;
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface IDataGridSettingsViewModel : IDisposable {

        int NumberOfItemsToDisplay { get; set; }

        ResizeObservableCollectionCycleModeEnum CycleMode { get; set; }
    }
}
