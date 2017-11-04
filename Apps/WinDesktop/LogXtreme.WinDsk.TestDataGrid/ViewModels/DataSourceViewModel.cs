
using System.Windows.Input;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Commands;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class DataSourceViewModel : IDataSourceViewModel {

        private readonly IDataSourceModel dataSourceModel;

        public DataSourceViewModel(IDataSourceModel dataSourceModel) {

            this.dataSourceModel = dataSourceModel;

            this.cmdReadNext = new RelayCommand();
        }

        public ICommand CommandReadNext => throw new System.NotImplementedException();

        public ICommand CommandStartReading => throw new System.NotImplementedException();

        public ICommand CommandStopReading => throw new System.NotImplementedException();
    }
}
