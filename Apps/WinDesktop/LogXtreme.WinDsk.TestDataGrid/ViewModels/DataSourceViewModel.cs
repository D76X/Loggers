
using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class DataSourceViewModel : IDataSourceViewModel {

        private readonly IDataSourceModel dataSourceModel;

        public DataSourceViewModel(IDataSourceModel dataSourceModel) {

            this.dataSourceModel = dataSourceModel;
        }
    }
}
