using System;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataSource2 : ISampleSource {

        private readonly ISampleDescriptor sampleDescriptor;
        private readonly ISampleGenerator sampleGenerator;

        public DataSource2(
            ISampleDescriptor sampleDescriptor,
            ISampleGenerator sampleGenerator) {

            this.sampleDescriptor = sampleDescriptor;
            this.sampleGenerator = sampleGenerator;
        }

        public ISampleDescriptor SampleDescriptor => throw new NotImplementedException();

        public ISample GetSample() {
            throw new NotImplementedException();
        }

        public IObservable<ISample> GetSamples() {
            throw new NotImplementedException();
        }
    }
}
