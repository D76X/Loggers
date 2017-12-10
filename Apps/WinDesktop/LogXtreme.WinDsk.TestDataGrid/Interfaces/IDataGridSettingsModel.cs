using LogXtreme.WinDsk.Infrastructure.Models;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes the settings for a data grid.
    /// </summary>
    public interface IDataGridSettingsModel {

        int NumberOfItemsToDisplay { get; set; }
        ResizeObservableCollectionCycleModeEnum CycleMode  { get; set; }
    }
}
