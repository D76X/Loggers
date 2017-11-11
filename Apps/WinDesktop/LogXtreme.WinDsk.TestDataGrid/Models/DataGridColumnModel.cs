using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataGridColumnModel : IDataGridColumnModel {

        public DataGridColumnModel(
            IHeaderModel headerModel,
            IDataGridColumnSettingsModel dataGridColumnSettingsModel) {

            this.Header = headerModel;
            this.ColumnSettings = dataGridColumnSettingsModel;
        }

        public IDataGridColumnSettingsModel ColumnSettings { get; set; }

        public IHeaderModel Header { get; set; }
    }
}
