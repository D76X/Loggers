using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Services {

    /// <summary>
    /// Refs
    /// http://si-w.co.uk/blog/2009/09/11/unit-testing-code-that-uses-a-backgroundworker/
    /// https://stackoverflow.com/questions/1411286/how-to-unit-test-backgroundworker-c-sharp
    /// https://stackoverflow.com/questions/2538065/what-is-the-basic-concept-behind-waithandle
    /// </summary>
    [TestClass]
    public class BackgroundDelegateWorkerTest {        

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRighParam_DoesWork_Completes() {

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

            if (mreOnStart.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.AreEqual(expected: valueOfParameterToPass, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);

            if (mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_DoesWork_ReportsProgress_Completes() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int, bool>(
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

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_DoesWork_ReportsProgressSteps_Completes() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int, bool>(
                supportReportProgress: true,
                supportCancellation: false);

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);

            const int valueOfParameterToPass = 99;
            const int initialValue = -1;
            var valueOfPassedParameter = initialValue;            
            var isWorkDone = false;
            var isWorkCompleted = false;

            const int initialProgress = 0;
            const int progressSteps = 5;             
            var progressTracker = new int[progressSteps] { 0,0,0,0,0 };           
            var progress = initialProgress;

            bdw.ProgressChange(p => {
                progress = p;
                progressTracker[p] = progress;
            });

            bdw.Process(
                i => {

                    valueOfPassedParameter = i;

                    var guard = 0;
                    while (progress < progressSteps && guard < 100) {
                        ++guard;
                        bdw.ReportProgress(++progress);
                        Thread.Sleep(1);
                    }

                    isWorkDone = progress == progressSteps;
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

            for (int i = 1; i <= progressTracker.Length; i++) {

                Assert.AreEqual(expected: i, actual: progressTracker[i-1]);
            }

            Assert.AreEqual(expected: valueOfParameterToPass, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);
            Assert.AreEqual(expected: progressSteps, actual: progress);

            if (mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_CancelsWork_DoesNotComplete() {

            Assert.Fail();
        }
    }
}
