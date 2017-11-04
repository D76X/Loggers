using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class DataGridColumnModel : IDataGridColumnModel {

        public string Header { get; set; }

        public bool IsVisible { get; set; }
    }
}
