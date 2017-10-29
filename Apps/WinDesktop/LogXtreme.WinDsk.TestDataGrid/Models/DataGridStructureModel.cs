using System.Collections.Generic;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class DataGridStructureModel : IDataGridStructureModel {

        public IEnumerable<IDataGridColumn> Columns => throw new System.NotImplementedException();

        public void Add(IDataGridColumn column) {
            throw new System.NotImplementedException();
        }

        public IDataGridColumn Remove(IDataGridColumn column) {
            throw new System.NotImplementedException();
        }
    }
}
