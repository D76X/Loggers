using LogXtreme.WinDsk.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Models {

    [TestClass]
    public class ResizeObservableCollectionTest {

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection() {
            
            // arrange
            Action actionToTest = () => new ResizeObservableCollection<int>(-1);

            // act & assert

        }
    }
}
