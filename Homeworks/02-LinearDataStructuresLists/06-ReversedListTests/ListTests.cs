namespace ReversedListTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _06_ReversedList;

    [TestClass]
    public class ListTests
    {
        private ReversedList<int> list;

        [TestInitialize]
        public void InitList()
        {
            this.list = new ReversedList<int>();
        }

        [TestMethod]
        public void TestRemove_WithPresentElements_ShouldRemoveElement()
        {
            const int ExpectedCount = 2;

            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(4);

            this.list.Remove(0);
            this.list.Remove(2);

            Assert.AreEqual(ExpectedCount, this.list.Count, "The list did not remove the item");
            Assert.IsTrue(
                this.list[0] == 3 &&
                this.list[1] == 2, "The list did not remove the item correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRemove_InvalidIndex_ShouldThrowException()
        {
            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(4);

            this.list.Remove(-1);
        }

        [TestMethod]
        public void TestCapacity_WithDefaultCapacity_ShouldReturnCorrectCapacity()
        {
            const int ExpectedCapacity = 16;

            this.list.Add(1);
            this.list.Add(2);

            Assert.AreEqual(ExpectedCapacity, this.list.Capacity, "The list do not return correct capacity");
        }

        [TestMethod]
        public void TestCapacity_WithSetCapacity_ShouldReturnCorrectCapacity()
        {
            ReversedList<int> testList = new ReversedList<int>(20);
            const int ExpectedCapacity = 20;

            testList.Add(1);
            testList.Add(2);

            Assert.AreEqual(ExpectedCapacity, testList.Capacity, "The list do not return correct capacity");
        }

        [TestMethod]
        public void TestCapacity_Resizing_ShouldResize()
        {
            ReversedList<int> testList = new ReversedList<int>(3);
            const int ExpectedCapacity = 3 * 2;

            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.AreEqual(ExpectedCapacity, testList.Capacity, "The list do not resize the capacity correctly");
        }

        [TestMethod]
        public void TestCount_WithPresentElements_ShouldReturnCorrectCount()
        {
            const int ExpectedCount = 4;

            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(41);

            Assert.AreEqual(ExpectedCount, this.list.Count, "The list do not return correct count after adding elements");
        }

        [TestMethod]
        public void TestReversedIndexation_WithPresentElements_ShouldReturnTheCorrectItem()
        {
            const int ExpectedElement = 5;

            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(4);
            this.list.Add(5);

            Assert.AreEqual(ExpectedElement, this.list[0], "The reversed indexation does not return correct items.");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestReversedIndexation_WithInvalidIndex_ShouldThrowException()
        {
            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);

            int a = this.list[3];
        }
    }
}
