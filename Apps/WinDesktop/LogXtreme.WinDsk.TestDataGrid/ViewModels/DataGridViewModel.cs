using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataGridViewModel : IDataGridViewModel {

        private readonly IDataGridModel dataGridModel;

        private ObservableCollection<IHeaderModel> headers;

        private ObservableCollection<IDataModel> data =
            new ObservableCollection<IDataModel>();

        public DataGridViewModel(IDataGridModel dataGridModel) {

            this.dataGridModel = dataGridModel;

            this.headers = new ObservableCollection<string>(
                this.dataSourceModel.DataDescriptor.ValueNames);
        }

        public ObservableCollection<IHeaderModel> Headers => 
            this.headers;

        public ObservableCollection<IDataModel> Data => 
            this.data;

        public IDataGridSettingsModel GridSettings => 
            this.dataGridModel.GridSettings;
    }
}
