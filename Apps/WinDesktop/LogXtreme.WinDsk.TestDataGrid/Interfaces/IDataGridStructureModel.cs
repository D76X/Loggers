
using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {
    public interface IDataGridStructureModel {

        IEnumerable<IDataGridColumn> Columns { get; }

        void Add(IDataGridColumn column);

        IDataGridColumn Remove(IDataGridColumn column);
    }
}
