
using System;
using System.Reactive.Subjects;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a data source.
    /// </summary>
    public interface IDataSourceModel {

        IDataDescriptorModel DataDescriptor { get; }

        void StartDataReads(int count);

        void StopDataReads();

        event EventHandler<IConnectableObservable<IDataModel>> OnStartDataReads;

        event EventHandler OnStopDataReads;
    }
}
