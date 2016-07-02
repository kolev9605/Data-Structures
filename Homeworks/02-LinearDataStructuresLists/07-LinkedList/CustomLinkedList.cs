namespace _07_LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomLinkedList<T> : IEnumerable<T>
    {
        public class ListNode<T>
        {
            public ListNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public ListNode<T> NextNode { get; set; }
        }

        public ListNode<T> Head { get; private set; }
        public ListNode<T> Tail { get; private set; }

        public int Count { get; private set; }

        public void Add(T element)
        {
            var node = new ListNode<T>(element);

            if (this.Count == 0)
            {
                this.Head = node;
                this.Tail = node;
            }
            else
            {
                this.Tail.NextNode = node;
                this.Tail = node;
            }

            this.Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException("The index is out of the range of the list.");
            }

            var currentNode = this.Head;
            ListNode<T> prevNode = null;
            int currentIndex = 0;

            while (currentNode != null)
            {
                if (currentIndex == index)
                {
                    if (prevNode == null)
                    {
                        this.Head = currentNode.NextNode;
                    }
                    else if (currentNode.NextNode == null)
                    {
                        this.Tail = prevNode;
                    }
                    else
                    {
                        prevNode.NextNode = currentNode.NextNode;    
                    }

                    this.Count--;
                    break;
                }

                currentIndex++;
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.Head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int FirstIndexOf(T element)
        {
            int indexOfElement = -1;
            int currentIndex = 0;

            var currentNode = this.Head;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(element))
                {
                    indexOfElement = currentIndex;
                    break;
                }

                currentIndex++;
                currentNode = currentNode.NextNode;
            }

            return indexOfElement;
        }

        public int LastIndexOf(T element)
        {
            int indexOfElement = -1;
            int currentIndex = 0;

            var currentNode = this.Head;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(element))
                {
                    indexOfElement = currentIndex;
                }

                currentIndex++;
                currentNode = currentNode.NextNode;
            }

            return indexOfElement;
        }
    }
}