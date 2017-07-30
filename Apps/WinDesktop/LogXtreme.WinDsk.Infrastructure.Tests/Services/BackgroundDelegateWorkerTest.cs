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
            var bdw = new BackgroundDelegateWorker<int, bool>();

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);

            var initialValue = -1;
            var valueOfPassedParameter = initialValue;
            var valueOfParameterToPass = 99;            
            var isWorkDone = false;
            var isWorkCompleted = false;

            bdw.Process(
                i => {
                    valueOfPassedParameter = i;
                    isWorkDone = true;
                    mreOnStart.Set();
                    return true;
                },
                r => {
                    isWorkCompleted = r;
                    mreOnCompleted.Set();
                });

            // act
            bdw.StartProcess(valueOfParameterToPass);

            // assert

            if (mreOnStart.WaitOne(new TimeSpan(0,0,1))) {};
            Assert.AreEqual(expected: valueOfParameterToPass, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);

            if (mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_ReportsProgress_And_Completes() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int,bool>(
                supportReportProgress: true,
                supportCancellation: false);

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);

            var initialValue = -1;
            var valueOfPassedParameter = initialValue;
            var valueOfParameterToPass = 99;
            var isWorkDone = false;
            var isWorkCompleted = false;

            var initialProgress = 0;
            var finalProgress = 100;
            var progress = initialProgress;
            
            bdw.ProgressChange(p => {
                progress = p;
            });

            bdw.Process(
                i => {
                    valueOfPassedParameter = i;
                    isWorkDone = true;
                    bdw.ReportProgress(finalProgress);
                    mreOnStart.Set();
                    return true;
                },
                r => {
                    isWorkCompleted = r;
                    mreOnCompleted.Set();
                });

            // act
            bdw.StartProcess(valueOfParameterToPass);

            // assert

            if (mreOnStart.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.AreEqual(expected: valueOfParameterToPass, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);
            Assert.AreEqual(expected: finalProgress, actual: progress);

            if (mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);

        }

    }
}
