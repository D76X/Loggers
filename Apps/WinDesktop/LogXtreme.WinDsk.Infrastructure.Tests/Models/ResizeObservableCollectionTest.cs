using LogXtreme.WinDsk.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogXtreme.Extensions;
using System;
using LogXtreme.Ifrastructure.Tests;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Models {

    [TestClass]
    public class ResizeObservableCollectionTest : TestBase {

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_Constructor_Throws_When_Size_Param_IsNegative() {

            // arrange
            Action actionToTest = () => new ResizeObservableCollection<int>(-1);
            string expectedMesssage = $"maxSize must be grater than 0 intead is -1";

            // act & assert   
            Assert.ThrowsException<ArgumentException>(
                actionToTest,
                expectedMesssage);
        }

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_DefaultConstructor_() {

            // arrange 

            // act 

            // assert
            Assert.Fail("test not implemented");
        }
    }
}
