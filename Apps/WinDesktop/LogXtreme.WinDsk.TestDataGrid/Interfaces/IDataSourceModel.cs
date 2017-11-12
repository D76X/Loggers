
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a data source.
    /// </summary>
    public interface IDataSourceModel {

        IDataDescriptorModel DataDescriptor { get; }

        void StartDataReads(int count);

        void StopDataReads();

        event EventHandler<IObservable<IDataModel>> OnStartDataReads;

        event EventHandler OnStopDataReads;
    }
}
