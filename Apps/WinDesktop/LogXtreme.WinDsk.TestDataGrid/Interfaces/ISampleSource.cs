
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleSource {

        ISample GetSample();

        IObservable<ISample> GetSamples();
    }
}
