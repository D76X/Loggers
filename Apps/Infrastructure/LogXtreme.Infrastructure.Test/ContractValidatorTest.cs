using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogXtreme.Infrastructure.ContractValidators;

namespace LogXtreme.Infrastructure.Test {

    [TestClass]
    public class ContractValidatorTest {

        /// <summary>
        /// Refs
        /// https://stackoverflow.com/questions/34242746/difference-between-icomparable-and-icomparablet-in-this-search-method
        /// </summary>
        private class GenComparable : IComparable<GenComparable> {

            public readonly int Value;

            public GenComparable(int val) {
                this.Value = val;
            }

            // with the generic IComparable<T> the CompareTo method 
            // as a parameter whose type is explicitly declared to 
            // be T. This is stricter at design time than the non 
            // generic IComparable
            public int CompareTo(GenComparable other) => 
                this.Value.CompareTo(other.Value);

            public override string ToString() =>
                $"{base.ToString()} with Value = {this.Value}";
        }

        private class Comparable : IComparable {

            public readonly int Value;

            public Comparable(int val) {
                this.Value = val;
            }

            // this takes a parameter of type object which in turns
            // allows this class to be compared to anything!
            public int CompareTo(object obj) =>
                this.Value.CompareTo(((Comparable)obj).Value);

            public override string ToString() =>
                $"{base.ToString()} with Value = {this.Value}";
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
            var testParameter5 = new GenComparable(1);
            var target5 = new GenComparable(0);
            var paramName5 = nameof(testParameter5);
            var validator5 = testParameter5.Validate(paramName5);

            // this time we must specify the type because IComparable<T> must 
            // be told what the comparable type T needs to be. That is T here 
            // is GenComparable so that the validator will know that to compare
            // the validator's value to the target it needs to cast both to 
            // IComparable<GenComparable>.
            Action actionToTest5 = () => validator5.EqualTo<GenComparable>(target5);

            string expectedMessage5 = $"{paramName5} should be equal to {target5} intead is {testParameter5}";

            // act & assert
            var exception5 = Assert.ThrowsException<ArgumentException>(
                actionToTest5,
                expectedMessage5);

            Assert.AreEqual(exception5.Message, expectedMessage5);

            // arrange - IComparable non generic interface
            // IComparable is the same as IComparable<object>
            var testParameter6 = new Comparable(1);
            var target6 = new Comparable(0);
            var paramName6 = nameof(testParameter6);
            var validator6 = testParameter6.Validate(paramName6);

            // this time because IComparable is not a generic interface 
            // we do not need to tell the validator the generic type it
            // needs to cast to. It will simply cast to IComparable to 
            // perform the comparison.
            Action actionToTest6 = () => validator6.EqualTo(target6);

            string expectedMessage6 = $"{paramName6} should be equal to {target6} intead is {testParameter6}";

            // act & assert
            var exception6 = Assert.ThrowsException<ArgumentException>(
                actionToTest6,
                expectedMessage6);

            Assert.AreEqual(exception6.Message, expectedMessage6);
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
