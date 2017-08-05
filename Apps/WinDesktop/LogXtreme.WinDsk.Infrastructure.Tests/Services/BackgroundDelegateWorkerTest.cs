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
                    return new DelegateWorkerResult<bool>(true);
                },
                r => {
                    isWorkCompleted = r.Result;
                    mreOnCompleted.Set();
                });

            // act
            bdw.StartProcess(valueOfParameterToPass);

            // assert

            if(mreOnStart.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);

            if(mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_DoesWork_ReportsProgress_Completes() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int, bool>(
                supportReportProgress: true,
                supportCancellation: false);

            var mreOnStart = new ManualResetEvent(false);
            var mreOnFinalProgress = new ManualResetEvent(false);
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
                mreOnFinalProgress.Set();
            });

            bdw.Process(
                i => {
                    valueOfPassedParameter = i;
                    isWorkDone = true;
                    bdw.ReportProgress(finalProgress);
                    mreOnStart.Set();
                    return new DelegateWorkerResult<bool>(true);
                },
                r => {
                    isWorkCompleted = r.Result;
                    mreOnCompleted.Set();
                });

            // act
            bdw.StartProcess(valueOfParameterToPass);

            // assert
            if(mreOnStart.WaitOne(new TimeSpan(0, 0, 1)) &&
                mreOnFinalProgress.WaitOne(new TimeSpan(0, 0, 1))) { };

            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);
            Assert.AreEqual(expected: finalProgress, actual: progress);

            if(mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
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
            var progressTracker = new int[progressSteps] { -1, -1, -1, -1, -1, -1 };

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
                    } while(progress < progressSteps && guard < 10);

                    isWorkDone = progress == progressSteps;
                    mreOnStart.Set();
                    return new DelegateWorkerResult<bool>(true);
                },
                r => {
                    isWorkCompleted = r.Result;
                    mreOnCompleted.Set();
                });

            // act
            bdw.StartProcess(valueOfParameterToPass);

            // assert
            if(mreOnStart.WaitOne(new TimeSpan(0, 0, 1))) { };

            Assert.AreEqual(expected: progressSteps, actual: progress);
            for(int i = 0; i < progressTracker.Length; i++) {
                Assert.AreEqual(expected: i, actual: progressTracker[i]);
            }

            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
            Assert.IsTrue(isWorkDone);

            if(mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(isWorkCompleted);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_CancelsWork_DoesNotComplete() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int, bool>(
                supportReportProgress: false,
                supportCancellation: true);

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);
            var mreOnCancelled = new ManualResetEvent(false);

            const int initialValue = 0;
            const int valueOfParameterToPass = initialValue;
            var valueOfPassedParameter = -100;
            const int allWorkToDo = 1000000;
            const int maxGuard = allWorkToDo * 10;

            var isWorkAllDone = false;
            var hasCompletedBeenCalled = false;
            var hasCancelledBeenCalled = false;
            var isCancelled = false;
            bool? returnedResult = null;

            bdw.Process(
                i => {

                    valueOfPassedParameter = i;

                    var guard = 0;
                    var workDone = 0;

                    while(workDone < allWorkToDo && guard < maxGuard) {

                        ++guard;

                        if(bdw.IsCancellationPending) {

                            // if cancellation is pending you might want to do some undo work
                            // or clean-up before breaking out the method that actually does 
                            // the work.

                            break;

                        } else {
                            ++workDone;
                        }
                    }

                    isWorkAllDone = bdw.IsCancellationPending ?
                                    false :
                                    workDone == allWorkToDo;

                    var result = new DelegateWorkerResult<bool>(isWorkAllDone);
                    result.Cancelled = bdw.IsCancellationPending;
                    mreOnStart.Set();
                    return result;
                },
                r => {

                    hasCompletedBeenCalled = true;
                    mreOnCompleted.Set();
                },
                r => {

                    hasCancelledBeenCalled = true;

                    if(r.Cancelled) {
                        isCancelled = true;
                    } else {
                        returnedResult = r.Result;
                    }

                    mreOnCancelled.Set();
                    mreOnCompleted.Set();
                });

            // act 
            bdw.StartProcess(valueOfParameterToPass);
            bdw.SignalCancellation();

            // assert

            if(mreOnStart.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.AreEqual(expected: initialValue, actual: valueOfPassedParameter);
            Assert.IsFalse(isWorkAllDone);

            if(mreOnCancelled.WaitOne(new TimeSpan(0, 0, 1))) { };
            Assert.IsTrue(hasCancelledBeenCalled);

            if(mreOnCompleted.WaitOne(new TimeSpan(0, 0, 1)) &&
                mreOnCancelled.WaitOne(new TimeSpan(0, 0, 1))) { };

            Assert.IsFalse(hasCompletedBeenCalled);
            Assert.IsTrue(isCancelled);
            Assert.IsFalse(returnedResult.HasValue);
        }

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRightParam_CancelsWorkAtMidStep_ReportProgress_DoesNotComplete() {

            // arrange           
            var bdw = new BackgroundDelegateWorker<int, bool>(
                supportReportProgress: true,
                supportCancellation: false);

            var mreOnStart = new ManualResetEvent(false);
            var mreOnCompleted = new ManualResetEvent(false);
            var mreOnCancelled = new ManualResetEvent(false);

            const int initialValue = 0;
            var valueOfPassedParameter = 99;
            const int valueOfParameterToPass = initialValue;

            var isWorkDone = false;
            var isWorkCompleted = false;

            var isWorkAllDone = false;
            var hasCompletedBeenCalled = false;
            var hasCancelledBeenCalled = false;
            var isCancelled = false;
            bool? returnedResult = null;

            const int initialProgress = 0;
            var progress = initialProgress;

            const int progressSteps = 7;
            const int midStepIndex = 3;
            var progressTracker = new int[progressSteps] { -1, -1, -1, -1, -1, -1, -1 };

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

                        if(progress = midStepIndex) {

                        }

                    } while(progress < progressSteps && guard < 10);

                    isWorkDone = progress == progressSteps;
                    mreOnStart.Set();
                    return new DelegateWorkerResult<bool>(true);
                },
                i => {

                },
                i => {

                });


            // act
            bdw.StartProcess(valueOfParameterToPass);
            bdw.SignalCancellation();

            // assert 
            Assert.Fail();
        }
    }
}
