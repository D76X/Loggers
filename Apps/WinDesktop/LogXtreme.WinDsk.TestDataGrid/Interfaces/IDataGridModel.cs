using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {
    public interface IDataGridModel {

        IEnumerable<IDataGridColumn> Columns { get; }

        IDataGridSettingsModel GridSettings { get; }
    }
}
