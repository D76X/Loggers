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

        public IDataGridStructureModel GridStructure => this.dataGridStructureModel;

        public IDataGridSettingsModel GridSettings => this.dataGridSettings;
    }
}
