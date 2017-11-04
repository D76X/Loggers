using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes the struture of a data grid.
    /// </summary>
    public interface IDataGridStructureModel {

        IEnumerable<IDataGridColumnModel> Columns { get; }        
    }
}
