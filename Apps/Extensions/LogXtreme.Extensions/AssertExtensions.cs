using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogXtreme.Extensions {

    public static class AssertExtensions {

        /// <summary>
        /// 
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/jj159340.aspx#sec18
        /// https://stackoverflow.com/questions/741029/best-way-to-test-exceptions-with-assert-to-ensure-they-will-be-thrown
        /// http://www.bradoncode.com/blog/2012/02/extending-assert-in-mstest.html
        /// 
        /// Asserting Exceptions in MSTest with Assert.Throws()
        /// http://www.bradoncode.com/blog/2012/01/asserting-exceptions-in-mstest-with.html
        /// </summary>
        /// <typeparam name="TException">type of the exception</typeparam>
        /// <param name="">the action tha is expected to throw the exception</param>
        public static void AssertThrows<TException>(
            this Assert assert, 
            Action action,
            string expectedMessage=null) where TException : Exception{

            try {

                action();
            }
            catch (Exception ex) {

                Assert.IsTrue(
                    ex.GetType() == typeof(TException),
                    $"Expected exception of type {typeof(TException)}  but type of {ex.GetType()}  was thrown instead.");

                if (expectedMessage != null) {

                    Assert.AreEqual(
                        expectedMessage, 
                        ex.Message,
                        $"Expected exception with a message of '{expectedMessage}' but exception with message of '{ex.Message}' was thrown instead.");
                }
                
                return;
            }

            Assert.Fail($"Expected exception of type {typeof(TException)} but no exception was thrown.");
        }
    }
}
