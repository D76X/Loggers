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

            const int initialValue = -1;
            const int valueOfParameterToPass = initialValue;
            var valueOfPassedParameter = 99;            

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
            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
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

            const int initialValue = -1;
            const int valueOfParameterToPass = initialValue;
            var valueOfPassedParameter = 99;
            
            var isWorkDone = false;
            var isWorkCompleted = false;

            const int initialProgress = 0;
            const int finalProgress = 100;
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

            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
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

            const int initialValue = 0;
            var valueOfPassedParameter = 99;
            const int valueOfParameterToPass = initialValue;               
            
            var isWorkDone = false;
            var isWorkCompleted = false;

            const int initialProgress = 0;
            var progress = initialProgress;

            const int progressSteps = 6;             
            var progressTracker = new int[progressSteps] { -1,-1,-1,-1,-1, -1 };                       

            bdw.ProgressChange(p => {                
                progressTracker[p] = p;                
            });

            bdw.Process(
                i => {

                    valueOfPassedParameter = i;

                    var guard = 0;

                    do {
                        ++guard;
                        bdw.ReportProgress(progress);
                        ++progress;
                        Thread.Sleep(1);
                    } while (progress < progressSteps && guard < 10);                    

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

            for (int i = 0; i < progressTracker.Length; i++) {                
                Assert.AreEqual(expected: i, actual: progressTracker[i]);
            }

            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
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
