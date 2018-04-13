using System;
using System.Reactive.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Refs
/// Observable factory methods
/// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#ObservableRange
/// How to implement an observer
/// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
/// Subjects
/// http://www.introtorx.com/Content/v1.0.10621.0/02_KeyTypes.html#Subject
/// http://reactivex.io/documentation/subject.html
/// </summary>
namespace LogXtreme.Reactive.Extensions.Test._1 {

    [TestClass]
    public class FlushOnTriggerTest {

        [TestMethod]
        public void TestMethod1() {

            int leftBound = 0;
            int rightBound = 10;
            int midPoint = 5;
            int bufferSize = 11;

            IObserver<int> observer = 

            IObservable<int> range = 
                Observable.Range(leftBound, rightBound);

            Func<int, bool> triggerPredicate = i => {
                return i > midPoint;
            };

            IObservable<int> bufferedRange = 
                range.FlushOnTrigger<int>(triggerPredicate, bufferSize);

            var subcriber = bufferedRange.Subscribe()
        }
    }
}
