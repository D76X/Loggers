using System.Collections.Generic;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class DataGridModel : IDataGridModel {

        private readonly IDataGridSettingsModel dataGridSettings;
        private readonly IDataGridStructureModel dataGridStructureModel;

        public DataGridModel(
            IDataGridStructureModel dataGridStructure,
            IDataGridSettingsModel dataGridSettings) {

            this.dataGridStructureModel = dataGridStructure;
            this.dataGridSettings = dataGridSettings;
        }

        public IEnumerable<IDataGridColumn> Columns => throw new System.NotImplementedException();

        public IDataGridSettingsModel GridSettings => throw new System.NotImplementedException();
    }
}
