using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Utils {

    /// <summary>
    /// 
    /// Refs
    /// 
    /// Reactive Extensions
    /// https://msdn.microsoft.com/en-us/library/hh242977(v=vs.103).aspx
    /// https://app.pluralsight.com/library/courses/reactive-extensions/table-of-contents
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

        public IObservable<ISample> GetSamples() {
            throw new NotImplementedException();
        }
    }
}
