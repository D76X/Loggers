using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataGridViewModel : IDataGridViewModel {

        private readonly IDataGridModel dataGridModel;

        public DataGridViewModel(IDataGridModel dataGridModel) {

            this.dataGridModel = dataGridModel;
        }
    }
}
