using LogXtreme.Ifrastructure.Tests;
using LogXtreme.WinDsk.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Models {

    [TestClass]
    public class ResizeObservableCollectionTest : TestBase {

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_DefaultConstructor_DefaultBehaviour() {

            // arrange 
            var collection = new ResizeObservableCollection<int>();
            var first = 1;
            var second = 2;
            var third = 3;

            // assert
            Assert.AreEqual(collection.MaxSize, 0);

            Assert.AreEqual(
                collection.Mode,
                ResizeObservableCollectionCycleModeEnum.None);

            Assert.AreEqual(collection.Count, 0);

            // act
            collection.Add(first);

            // assert
            Assert.AreEqual(collection.Count, 1);
            Assert.AreEqual(collection.IndexOf(first), 0);

            // act
            collection.Add(second);

            // assert
            Assert.AreEqual(collection.Count, 2);
            Assert.AreEqual(collection.IndexOf(second), 1);

            // act
            collection.Add(third);

            // assert
            Assert.AreEqual(collection.Count, 3);
            Assert.AreEqual(collection.IndexOf(third), 2);

            // act
            var previousCount = collection.Count;
            collection.RemoveAt(0);

            // assert
            Assert.IsFalse(collection.Contains(first));
            Assert.AreEqual(collection.Count, previousCount - 1);
            Assert.AreEqual(collection.IndexOf(second), 0);
            Assert.AreEqual(collection.IndexOf(third), 1);
        }

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_Contructor_Thorws_When_MaxSize_Is_Negative() {

            // arrange
            int maxSize = -1;
            Action actionToTest = () => new ResizeObservableCollection<int>(maxSize);
            string expectedMesssage = $"maxSize must be grater than 0 intead is {maxSize}";

            // act & assert   
            Assert.ThrowsException<ArgumentException>(
                actionToTest,
                expectedMesssage);
        }

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_Contructor_Thorws_When_MaxSize_Is_Zero() {

            // arrange
            int maxSize = 0;
            Action actionToTest = () => new ResizeObservableCollection<int>(maxSize);
            string expectedMesssage = $"maxSize must be grater than 0 intead is {maxSize}";

            // act & assert   
            Assert.ThrowsException<ArgumentException>(
                actionToTest,
                expectedMesssage);
        }

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_SetMaxSize_DefaultMode_Behaviour_Queue() {

            // arrange
            int maxSize = 3;

            var collection = new ResizeObservableCollection<int>(maxSize);

            var first = 1;
            var second = 2;
            var third = 3;

            var fourth = 4;
            var fifth = 5;
            var sixth = 6;

            // act
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // assert 
            Assert.AreEqual(collection.Count, maxSize);
            Assert.AreEqual(collection.Mode, ResizeObservableCollectionCycleModeEnum.Queue);
            Assert.AreEqual(collection.IndexOf(first), 0);
            Assert.AreEqual(collection.IndexOf(second), 1);
            Assert.AreEqual(collection.IndexOf(third), 2);

            // act - first queue overflow
            collection.Add(fourth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.AreEqual(collection.IndexOf(second), 0);
            Assert.AreEqual(collection.IndexOf(third), 1);
            Assert.AreEqual(collection.IndexOf(fourth), 2);

            // act - second queue overflow
            collection.Add(fifth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.IsFalse(collection.Contains(second));
            Assert.AreEqual(collection.IndexOf(third), 0);
            Assert.AreEqual(collection.IndexOf(fourth), 1);
            Assert.AreEqual(collection.IndexOf(fifth), 2);

            // act - third queue overflow
            collection.Add(sixth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.IsFalse(collection.Contains(second));
            Assert.IsFalse(collection.Contains(third));
            Assert.AreEqual(collection.IndexOf(fourth), 0);
            Assert.AreEqual(collection.IndexOf(fifth), 1);
            Assert.AreEqual(collection.IndexOf(sixth), 2);
        }


        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_SetMaxSize_SetMode_Queue() {

            // arrange
            int maxSize = 3;

            var collection = new ResizeObservableCollection<int>(
                maxSize,
                ResizeObservableCollectionCycleModeEnum.Queue);

            var first = 1;
            var second = 2;
            var third = 3;

            var fourth = 4;
            var fifth = 5;
            var sixth = 6;

            // act
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // assert 
            Assert.AreEqual(collection.Count, maxSize);
            Assert.AreEqual(collection.Mode, ResizeObservableCollectionCycleModeEnum.Queue);
            Assert.AreEqual(collection.IndexOf(first), 0);
            Assert.AreEqual(collection.IndexOf(second), 1);
            Assert.AreEqual(collection.IndexOf(third), 2);

            // act - first queue overflow
            collection.Add(fourth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.AreEqual(collection.IndexOf(second), 0);
            Assert.AreEqual(collection.IndexOf(third), 1);
            Assert.AreEqual(collection.IndexOf(fourth), 2);

            // act - second queue overflow
            collection.Add(fifth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.IsFalse(collection.Contains(second));
            Assert.AreEqual(collection.IndexOf(third), 0);
            Assert.AreEqual(collection.IndexOf(fourth), 1);
            Assert.AreEqual(collection.IndexOf(fifth), 2);

            // act - third queue overflow
            collection.Add(sixth);

            // assert    
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.IsFalse(collection.Contains(second));
            Assert.IsFalse(collection.Contains(third));
            Assert.AreEqual(collection.IndexOf(fourth), 0);
            Assert.AreEqual(collection.IndexOf(fifth), 1);
            Assert.AreEqual(collection.IndexOf(sixth), 2);
        }

        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_SetMaxSize_SetMode_Flush() {

            // arrange
            int maxSize = 3;

            var collection = new ResizeObservableCollection<int>(
                maxSize,
                ResizeObservableCollectionCycleModeEnum.Flush);

            var first = 1;
            var second = 2;
            var third = 3;

            var fourth = 4;
            var fifth = 5;
            var sixth = 6;

            // act
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // assert 
            Assert.AreEqual(collection.Count, maxSize);
            Assert.AreEqual(collection.Mode, ResizeObservableCollectionCycleModeEnum.Flush);
            Assert.AreEqual(collection.IndexOf(first), 0);
            Assert.AreEqual(collection.IndexOf(second), 1);
            Assert.AreEqual(collection.IndexOf(third), 2);

            // act - first buffer overflow 
            collection.Add(fourth);

            // assert
            Assert.AreEqual(collection.Count, 1);
            Assert.AreEqual(collection.Mode, ResizeObservableCollectionCycleModeEnum.Flush);
            Assert.IsFalse(collection.Contains(first));
            Assert.IsFalse(collection.Contains(second));
            Assert.IsFalse(collection.Contains(third));
            Assert.AreEqual(collection.IndexOf(fourth), 0);

            // act
            collection.Add(fifth);

            // assert
            Assert.AreEqual(collection.Count, 2);
            Assert.AreEqual(collection.IndexOf(fourth), 0);
            Assert.AreEqual(collection.IndexOf(fifth), 1);

            // act
            collection.Add(sixth);

            // assert
            Assert.AreEqual(collection.Count, 3);
            Assert.AreEqual(collection.IndexOf(fourth), 0);
            Assert.AreEqual(collection.IndexOf(fifth), 1);
            Assert.AreEqual(collection.IndexOf(sixth), 2);

            // act - second overflow
            collection.Add(first);
            Assert.AreEqual(collection.Count, 1);
            Assert.IsFalse(collection.Contains(fourth));
            Assert.IsFalse(collection.Contains(fifth));
            Assert.IsFalse(collection.Contains(sixth));
            Assert.AreEqual(collection.IndexOf(first), 0);
        }


        [TestMethod]
        public void InfiniteSizeResizeObservableCollection_SetMaxSize_SetMode_Roll() {

            // arrange
            int maxSize = 3;

            var collection = new ResizeObservableCollection<int>(
                maxSize,
                ResizeObservableCollectionCycleModeEnum.Roll);

            var first = 1;
            var second = 2;
            var third = 3;

            var fourth = 4;
            var fifth = 5;
            var sixth = 6;

            // act
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // assert 
            Assert.AreEqual(collection.Count, maxSize);
            Assert.AreEqual(collection.Mode, ResizeObservableCollectionCycleModeEnum.Roll);
            Assert.AreEqual(collection.IndexOf(first), 0);
            Assert.AreEqual(collection.IndexOf(second), 1);
            Assert.AreEqual(collection.IndexOf(third), 2);

            // act - first overflow
            collection.Add(fourth);

            // assert
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(third));
            Assert.AreEqual(collection.IndexOf(fourth), 0);
            Assert.AreEqual(collection.IndexOf(first), 1);
            Assert.AreEqual(collection.IndexOf(second), 2);

            // act - second overflow
            collection.Add(fifth);

            // assert
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(second));
            Assert.AreEqual(collection.IndexOf(fifth), 0);
            Assert.AreEqual(collection.IndexOf(fourth), 1);
            Assert.AreEqual(collection.IndexOf(first), 2);

            // act - third overflow
            collection.Add(sixth);

            // assert
            Assert.AreEqual(collection.Count, maxSize);
            Assert.IsFalse(collection.Contains(first));
            Assert.AreEqual(collection.IndexOf(sixth), 0);
            Assert.AreEqual(collection.IndexOf(fifth), 1);
            Assert.AreEqual(collection.IndexOf(fourth), 2);
        }
    }
}
