using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a data grid.
    /// </summary>
    public interface IDataGridModel {

        IDataGridStructureModel GridStructure { get; }

        IDataGridSettingsModel GridSettings { get; }
    }
}
