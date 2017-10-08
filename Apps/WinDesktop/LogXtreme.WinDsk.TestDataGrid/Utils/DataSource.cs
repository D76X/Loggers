using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Utils {

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
    public class DataSource : ISampleSource {

        private readonly Random generator;

        public DataSource() {
            this.generator = new Random();            
        }        

        public ISample GetSample() {            
            return new Sample(new string[3] { "X", "Y", "Z" });
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
            Func<int, ISample> resultSelector = i => new Sample(new string[3] { "X", "Y", "Z" });

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
    }
}
