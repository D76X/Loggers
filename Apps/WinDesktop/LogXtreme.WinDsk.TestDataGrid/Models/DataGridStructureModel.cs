using System.Collections.Generic;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataGridStructureModel : IDataGridStructureModel {

        private readonly IEnumerable<IDataGridColumnModel> columns;

        public DataGridStructureModel(
            IEnumerable<IDataGridColumnModel> columns) {

            this.columns = columns; 
        }

        public IEnumerable<IDataGridColumnModel> Columns => this.columns;        
    }
}
