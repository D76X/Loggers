using System;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface IDataGridViewModel : IDisposable {

        ObservableCollection<IHeaderModel> Headers { get; }

        ObservableCollection<IDataModel> Data { get; }

        IDataGridSettingsViewModel GridSettings { get; }
    }
}
