using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _03_ArrayBasedStackTests
{
    using _03_ArrayBasedStack;

    [TestClass]
    public class ArrayStackTests
    {
        [TestMethod]
        public void ArrayStack_PushPopEmptyStack_ShouldAdd()
        {
            ArrayStack<int> stack = new ArrayStack<int>();
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
        public void ArrayStack_PushManyElementsEmptyStack_ShouldAddAndResize()
        {
            ArrayStack<int> stack = new ArrayStack<int>();

            for (int i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(1000, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ArrayStack_PopEmptyStack_ShouldThrowException()
        {
            ArrayStack<int> stack = new ArrayStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void ArrayStack_ToArrayPresentElements_ShouldReturnCorrectArray()
        {
            ArrayStack<int> stack = new ArrayStack<int>();
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
        public void ArrayStack_ToArrayEmptyStack_ShouldReturnEmptyArray()
        {
            ArrayStack<int> stack = new ArrayStack<int>();

            int[] expected = { };
            var array = stack.ToArray();

            CollectionAssert.AreEqual(expected, array);
        }
    }
}
