using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogXtreme.Infrastructure.ContractValidators;

namespace LogXtreme.Infrastructure.Test {

    [TestClass]
    public class ContractValidatorTest {

        [TestMethod]
        public void ContractValidator_NotNull_Throws_When_Argument_Is_Null() {

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
        public void ContractValidator_NotEmpty_Throws_ArgumentNullException_When_Argument_Is_NotString() {

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

            // act 

            // assert
            Assert.Fail("test not implemented");
        }

        [TestMethod]
        public void ContractValidator_NotWhiteSpace_Throws_ArgumentNullException_When_Argument_Is_Not_String() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }

        [TestMethod]
        public void ContractValidator_EqualTo_Throws_ArgumentException_When_Argument_Is_Not_Equal() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
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
