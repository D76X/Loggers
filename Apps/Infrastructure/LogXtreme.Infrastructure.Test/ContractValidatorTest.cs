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
            string expectedMessage = "Value cannot be null.\r\nParameter name: testParameter";


            // act & assert
            var exception = Assert.ThrowsException<ArgumentNullException>(
                actionToTest,
                expectedMessage);

            Assert.AreEqual(exception.Message, expectedMessage);
        }
    }
}
