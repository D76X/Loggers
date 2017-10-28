using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    /// <summary> 
    /// 
    /// Refs
    /// 
    /// Reactive Extensions
    /// https://msdn.microsoft.com/en-us/library/hh242977(v=vs.103).aspx
    /// https://app.pluralsight.com/library/courses/reactive-extensions/table-of-contents
    /// http://www.introtorx.com/
    /// https://stackoverflow.com/questions/14396449/why-are-subjects-not-recommended-in-net-reactive-extensions
    /// 
    /// Generate Random Numbers
    /// https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-int-number-in-c
    /// 
    /// </summary>
    public class DataSource1 : ISampleSource {
        
        private readonly ISampleDescriptor sampleDescriptor;
        private readonly ISampleGenerator sampleGenerator;

        public DataSource1(
            ISampleDescriptor sampleDescriptor,
            ISampleGenerator sampleGenerator) {          
            
            this.sampleDescriptor = sampleDescriptor;
            this.sampleGenerator = sampleGenerator;
        }

        public ISampleDescriptor SampleDescriptor => this.sampleDescriptor;

        public ISample GetSample() {

            return this.DrawSample();
        }

        /// <summary>
        /// Refs
        /// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html
        /// </summary>
        /// <returns></returns>
        public IObservable<ISample> GetSamples() {

            int initialState = 0;
            Func<int, bool> executeNextIteration = i => true;
            Func<int, int> coresursion = i => i + 1;
            Func<int, ISample> resultSelector = i => this.DrawSample();

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
        
        private  ISample DrawSample() {            
            return this.sampleGenerator.GenerateSample(this.sampleDescriptor);
        }
    }
}
