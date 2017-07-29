using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Services {

    [TestClass]
    public class BackgroundDelegateWorkerTest {

        [TestMethod]
        public void BackgroundDelegateWorker_PassesRighParam_DoesWork_And_Completes() {

            // arrange
            var bdw = new BackgroundDelegateWorker();

            var parameterToPass = 99;
            var passedParameter = -1;
            var isWorkDone = false;
            var isCompleted = false;

            // act
            bdw.Start<int, bool>(
                i => {
                    passedParameter = i;
                    isWorkDone = true;
                    return true;
                },
                r => {
                    isCompleted = r;
                },
                parameterToPass);

            // assert
            Assert.AreEqual(passedParameter, parameterToPass);
            Assert.IsTrue(isWorkDone);
            Assert.IsTrue(isCompleted);
        }

    }
}
