
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleSourceModel {

        ISampleDescriptorModel SampleDescriptor{ get; }

        ISampleModel GetSample();

        IObservable<ISampleModel> GetSamples();
    }
}
