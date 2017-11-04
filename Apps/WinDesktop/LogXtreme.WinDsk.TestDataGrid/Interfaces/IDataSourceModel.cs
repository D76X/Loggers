
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a data source.
    /// </summary>
    public interface IDataSourceModel {

        IDataDescriptorModel DataDescriptor { get; }

        IDataModel GetData();

        IObservable<IDataModel> GetDatas();
    }
}
