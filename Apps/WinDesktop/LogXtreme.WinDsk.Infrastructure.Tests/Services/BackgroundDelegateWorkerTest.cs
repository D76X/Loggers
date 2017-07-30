using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Services {

    /// <summary>
    /// 
    /// Refs
    /// http://si-w.co.uk/blog/2009/09/11/unit-testing-code-that-uses-a-backgroundworker/
    /// https://stackoverflow.com/questions/1411286/how-to-unit-test-backgroundworker-c-sharp
    /// https://stackoverflow.com/questions/2538065/what-is-the-basic-concept-behind-waithandle
    /// </summary>
    [TestClass]
    public class BackgroundDelegateWorkerTest {

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRighParam_DoesWork_And_Completes() {

            // arrange
            var bdw = new BackgroundDelegateWorker();

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);

            var initialValue = -1;
            var valueOfPassedParameter = initialValue;
            var valueOfParameterToPass = 99;            
            var isWorkDone = false;
            var isWorkCompleted = false;

            // act
            bdw.Start<int, bool>(
                i => {
                    valueOfPassedParameter = i;
                    isWorkDone = true;
                    mreOnStart.Set();
                    return true;
                },
                r => {
                    isWorkCompleted = r;
                    mreOnCompleted.Set();
                },
                valueOfParameterToPass);

            // assert

            if (mreOnStart.WaitOne(new TimeSpan(0,0,1))) {};
            Assert.AreEqual(valueOfPassedParameter, valueOfParameterToPass);
            Assert.IsTrue(isWorkDone);

            if (mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

    }
}
