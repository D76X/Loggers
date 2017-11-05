using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface IDataGridViewModel {

        ObservableCollection<IHeaderModel> Headers { get; }

        ObservableCollection<IDataModel> Data { get; }

        IDataGridSettingsModel GridSettings { get; }
    }
}
