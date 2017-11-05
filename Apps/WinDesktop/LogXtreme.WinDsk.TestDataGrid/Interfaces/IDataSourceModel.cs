
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a data source.
    /// </summary>
    public interface IDataSourceModel {

        IDataDescriptorModel DataDescriptor { get; }

        //IObservable<IObservable<IDataModel>> GetData(int count);

        //IDataModel GetData();

        //IObservable<IDataModel> GetData(int count);

        void StartDataReads(int count);

        void StopDataReads();

        event EventHandler<IObservable<IDataModel>> OnStartDataReads;

        event EventHandler OnStopDataReads;
    }
}
