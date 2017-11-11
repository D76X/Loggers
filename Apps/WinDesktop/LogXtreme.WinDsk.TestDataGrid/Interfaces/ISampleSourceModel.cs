
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a source of samples.
    /// </summary>
    public interface ISampleSourceModel {

        ISampleDescriptorModel SampleDescriptor{ get; }
        IObservable<ISampleModel> GetSamples(int count);
    }
}
