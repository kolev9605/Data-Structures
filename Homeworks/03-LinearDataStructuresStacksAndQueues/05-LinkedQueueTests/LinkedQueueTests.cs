namespace _05_LinkedQueueTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _05_LinkedQueue;

    [TestClass]
    public class LinkedQueueTests
    {
        [TestMethod]
        public void LinkedQueue_EnqueueDequeueElements_ShouldBehieveCorrectly()
        {
            LinkedQueue<int> stack = new LinkedQueue<int>();
            Assert.AreEqual(0, stack.Count);
            stack.Enqueue(2);
            Assert.AreEqual(1, stack.Count);
            stack.Enqueue(10);
            Assert.AreEqual(2, stack.Count);
            stack.Dequeue();
            Assert.AreEqual(1, stack.Count);
            stack.Dequeue();
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void LinkedQueue_EnqueueManyElementsEmptyStack_ShouldAdd()
        {
            LinkedQueue<int> stack = new LinkedQueue<int>();

            for (int i = 0; i < 1000; i++)
            {
                stack.Enqueue(i);
            }

            Assert.AreEqual(1000, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinkedQueue_DequeueEmptyStack_ShouldThrowException()
        {
            LinkedQueue<int> stack = new LinkedQueue<int>();
            stack.Dequeue();
        }

        [TestMethod]
        public void LinkedQueue_ToArrayPresentElements_ShouldReturnCorrectArray()
        {
            LinkedQueue<int> stack = new LinkedQueue<int>();
            stack.Enqueue(5);
            stack.Enqueue(4);
            stack.Enqueue(3);
            stack.Enqueue(2);
            stack.Enqueue(1);
            stack.Enqueue(0);

            int[] expected = { 5, 4, 3, 2, 1, 0 };
            var array = stack.ToArray();

            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void LinkedQueue_ToArrayEmptyStack_ShouldReturnEmptyArray()
        {
            LinkedQueue<int> stack = new LinkedQueue<int>();

            int[] expected = { };
            var array = stack.ToArray();

            CollectionAssert.AreEqual(expected, array);
        }
    }
}
