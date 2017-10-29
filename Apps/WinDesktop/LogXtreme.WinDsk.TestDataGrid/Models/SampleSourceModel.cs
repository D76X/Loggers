using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class SampleSourceModel : ISampleSourceModel {

        private readonly ISampleDescriptorModel sampleDescriptor;
        private readonly ISampleGeneratorModel sampleGenerator;

        public SampleSourceModel(
            ISampleDescriptorModel sampleDescriptor,
            ISampleGeneratorModel sampleGenerator) {

            this.sampleDescriptor = sampleDescriptor;
            this.sampleGenerator = sampleGenerator;
        }

    public ISampleDescriptorModel SampleDescriptor => throw new NotImplementedException();

        public ISampleModel GetSample() {
            return this.DrawSample();
        }

        public IObservable<ISampleModel> GetSamples() {

            int initialState = 0;
            Func<int, bool> executeNextIteration = i => true;
            Func<int, int> coresursion = i => i + 1;
            Func<int, ISampleModel> resultSelector = i => this.DrawSample();

            var duetime = TimeSpan.FromMilliseconds(500);
            var interval = TimeSpan.FromSeconds(1);
            Func<int, TimeSpan> timeSelector = i => i == 0 ? duetime : interval;

            var source = Observable.Generate(
                initialState,
                executeNextIteration,
                coresursion,
                resultSelector,
                timeSelector);

            return source;
        }

        private ISampleModel DrawSample() {
            return this.sampleGenerator.GenerateSample(this.sampleDescriptor);
        }
    }
}
