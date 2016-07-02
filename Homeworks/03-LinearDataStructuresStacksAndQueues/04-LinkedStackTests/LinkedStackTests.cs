namespace _04_LinkedStackTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedStackTests
    {
        [TestMethod]
        public void LinkedStack_PushPopEmptyStack_ShouldAdd()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            Assert.AreEqual(0, stack.Count);
            stack.Push(2);
            Assert.AreEqual(1, stack.Count);
            stack.Push(10);
            Assert.AreEqual(2, stack.Count);
            stack.Pop();
            Assert.AreEqual(1, stack.Count);
            stack.Pop();
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void LinkedStack_PushManyElementsEmptyStack_ShouldAddAndResize()
        {
            LinkedStack<int> stack = new LinkedStack<int>();

            for (int i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(1000, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinkedStack_PopEmptyStack_ShouldThrowException()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void LinkedStack_ToArrayPresentElements_ShouldReturnCorrectArray()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            stack.Push(5);
            stack.Push(4);
            stack.Push(3);
            stack.Push(2);
            stack.Push(1);
            stack.Push(0);

            int[] expected = { 0, 1, 2, 3, 4, 5 };
            var array = stack.ToArray();

            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void LinkedStack_ToArrayEmptyStack_ShouldReturnEmptyArray()
        {
            LinkedStack<int> stack = new LinkedStack<int>();

            int[] expected = { };
            var array = stack.ToArray();

            CollectionAssert.AreEqual(expected, array);
        }
    }
}
