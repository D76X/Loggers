
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleSource {

        ISampleDescriptor SampleDescriptor{ get; }

        ISample GetSample();

        IObservable<ISample> GetSamples();
    }
}
