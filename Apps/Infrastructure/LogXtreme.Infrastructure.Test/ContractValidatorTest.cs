using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogXtreme.Infrastructure.ContractValidators;

namespace LogXtreme.Infrastructure.Test {

    [TestClass]
    public class ContractValidatorTest {

        private class Comparable<T> : IComparable<T> {            

            public int CompareTo(T other) {
                throw new NotImplementedException();
            }
        }

        private class Comparable : IComparable {

            public int CompareTo(object obj) {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void ContractValidator_NotNull_Throws_ArgumentNullException_When_Argument_Is_Null() {

            // arrange 
            object testParameter = null;
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotNull();
            string expectedMessage = $"Value cannot be null.\r\nParameter name: {paramName}";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotEmpty_Throws_ArgumentException_When_Argument_Is_Empty_String() {

            // arrange 
            string testParameter = string.Empty;
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotEmpty();
            string expectedMessage = $"{paramName} is an empty string";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotEmpty_Throws_ArgumentNullException_When_Argument_Is_Not_String() {

            // arrange 
            object testParameter = new object { };
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotEmpty();
            string expectedMessage = $"Value cannot be null.\r\nParameter name: {paramName}";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotEmpty_Throws_ArgumentNullException_When_Argument_Is_Null() {

            // arrange 
            string testParameter = null;
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotEmpty();
            string expectedMessage = $"Value cannot be null.\r\nParameter name: {paramName}";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotWhiteSpace_Throws_ArgumentException_When_Argument_Is_WhiteSpace() {

            // arrange 
            string testParameter = "   \r\n ";
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotWhiteSpace();
            string expectedMessage = $"{paramName} is white space";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotWhiteSpace_Throws_ArgumentNullException_When_Argument_Is_Not_String() {

            // arrange 
            object testParameter = new object { };
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotWhiteSpace();
            string expectedMessage = $"Value cannot be null.\r\nParameter name: {paramName}";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_NotWhiteSpace_Throws_ArgumentNullException_When_Argument_Is_Null() {

            // arrange 
            string testParameter = null;
            var paramName = nameof(testParameter);
            var validator = testParameter.Validate(paramName);
            Action actionToTest = () => validator.NotWhiteSpace();
            string expectedMessage = $"Value cannot be null.\r\nParameter name: {paramName}";

            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }

        [TestMethod]
        public void ContractValidator_EqualTo_Throws_ArgumentException_When_Argument_Is_Not_Equal() {

            // arrange - compare strings
            var testParameter1 = "test-1";
            var target1 = "taget-1";
            var paramName1 = nameof(testParameter1);
            var validator1 = testParameter1.Validate(paramName1);
            Action actionToTest1 = () => validator1.EqualTo(target1);
            string expectedMessage1 = $"{paramName1} should be equal to {target1} intead is {testParameter1}";

            // act & assert
            var exception1 = Assert.ThrowsException<ArgumentException>(
                actionToTest1,
                expectedMessage1);

            Assert.AreEqual(exception1.Message, expectedMessage1);

            // arrange - compare integers
            var testParameter2 = 1;
            var target2 = 0;
            var paramName2 = nameof(testParameter2);
            var validator2 = testParameter2.Validate(paramName2);
            Action actionToTest2 = () => validator2.EqualTo(target2);
            string expectedMessage2 = $"{paramName2} should be equal to {target2} intead is {testParameter2}";

            // act & assert
            var exception2 = Assert.ThrowsException<ArgumentException>(
                actionToTest2,
                expectedMessage2);

            Assert.AreEqual(exception2.Message, expectedMessage2);

            // arrange - compare double
            var testParameter3 = 1.00;
            var target3 = 0.00;
            var paramName3 = nameof(testParameter3);
            var validator3 = testParameter3.Validate(paramName3);
            Action actionToTest3 = () => validator3.EqualTo(target3);
            string expectedMessage3 = $"{paramName3} should be equal to {target3} intead is {testParameter3}";

            // act & assert
            var exception3 = Assert.ThrowsException<ArgumentException>(
                actionToTest3,
                expectedMessage3);

            Assert.AreEqual(exception3.Message, expectedMessage3);

            // arrange - compare decimal
            var testParameter4 = 1.00d;
            var target4 = 0.00d;
            var paramName4 = nameof(testParameter4);
            var validator4 = testParameter4.Validate(paramName4);
            Action actionToTest4 = () => validator4.EqualTo(target4);
            string expectedMessage4 = $"{paramName4} should be equal to {target4} intead is {testParameter4}";

            // act & assert
            var exception4 = Assert.ThrowsException<ArgumentException>(
                actionToTest4,
                expectedMessage4);

            Assert.AreEqual(exception4.Message, expectedMessage4);

            // arrange - IComparable<T>
        }

        [TestMethod]
        public void ContractValidator_GreaterThan_Throws_ArgumentException_When_Argument_Is_Not_GreaterThan() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }

        [TestMethod]
        public void ContractValidator_GreaterThanOrEqual_Throws_ArgumentException_When_Argument_Is_Not_GreaterThanOrEqual() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }

        [TestMethod]
        public void ContractValidator_SmallerThan_Throws_ArgumentException_When_Argument_Is_Not_SmallerThan() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }

        [TestMethod]
        public void ContractValidator_SmallerThanOrEqual_Throws_ArgumentException_When_Argument_Is_Not_SmallerThanOrEqual() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }
    }
}
