namespace _07_LinkedListTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _07_LinkedList;

    [TestClass]
    public class LinledListTests
    {
        [TestMethod]
        public void TestAdd_EmptyList_ShouldAdd()
        {
            CustomLinkedList<int> list = new CustomLinkedList<int>();

            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(16);

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(list.Head.Value, 5);
            Assert.AreEqual(list.Tail.Value, 16);
        }

        [TestMethod]
        public void TestRemoveAtIndex_ZeroIndex_ShouldRemove()
        {
            CustomLinkedList<int> list = new CustomLinkedList<int>();

            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(16);

            list.Remove(0);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(6, list.Head.Value);
        }

        [TestMethod]
        public void TestRemoveAtIndex_LastIndex_ShouldRemove()
        {
            CustomLinkedList<int> list = new CustomLinkedList<int>();

            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(16);

            list.Remove(list.Count - 1);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(8, list.Tail.Value);
        }

        [TestMethod]
        public void IEnumerable_Foreach_MultipleElements()
        {
            // Arrange
            var list = new CustomLinkedList<string>();
            list.Add("Five");
            list.Add("Six");
            list.Add("Seven");

            // Act
            var items = new List<string>();
            foreach (var element in list)
            {
                items.Add(element);
            }

            // Assert
            CollectionAssert.AreEqual(items,
                new List<string>() { "Five", "Six", "Seven" });
        }

        [TestMethod]
        public void FirstIndexOf_ExistingItem_ShouldReturnIndex()
        {
            // Arrange
            var list = new CustomLinkedList<string>();
            list.Add("Five");
            list.Add("Six");
            list.Add("Seven");
            list.Add("Six");
            list.Add("Six");

            // Act
            int index = list.FirstIndexOf("Six");

            // Assert
            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void LastIndexOf_ExistingItem_ShouldReturnIndex()
        {
            // Arrange
            var list = new CustomLinkedList<string>();
            list.Add("Five");
            list.Add("Six");
            list.Add("Seven");
            list.Add("Six");
            list.Add("Six");

            // Act
            int index = list.LastIndexOf("Six");

            // Assert
            Assert.AreEqual(4, index);
        }

        [TestMethod]
        public void FirstIndexOf_NonExistingItem_ShouldReturnCorrectErrorNumber()
        {
            // Arrange
            var list = new CustomLinkedList<string>();
            list.Add("Five");
            list.Add("Six");
            list.Add("Seven");
            list.Add("Six");
            list.Add("Six");

            // Act
            int index = list.FirstIndexOf("Eight");

            // Assert
            Assert.AreEqual(-1, index);
        }
    }
}
