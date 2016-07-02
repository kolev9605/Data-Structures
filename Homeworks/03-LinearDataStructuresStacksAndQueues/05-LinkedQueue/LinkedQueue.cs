using System;

namespace _05_LinkedQueue
{
    public class LinkedQueue<T>
    {
        private QueueNode<T> firstNode;
        private QueueNode<T> lastNode;

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            var node = new QueueNode<T>(element);

            if (this.Count == 0)
            {
                this.firstNode = node;
                this.lastNode = node;
            }
            else
            {
                this.lastNode.NextNode = node;
                node.PrevNode = this.lastNode;
                this.lastNode = node;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var nodeToReturn = this.firstNode;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;

            return nodeToReturn.Value;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            var current = this.firstNode;
            int index = 0;

            while (current != null)
            {
                array[index] = current.Value;
                index++;
                current = current.NextNode;
            }

            return array;
        }

        private class QueueNode<T>
        {
            public QueueNode(T element)
            {
                this.Value = element;
            }

            public T Value { get; private set; }

            public QueueNode<T> NextNode { get; set; }

            public QueueNode<T> PrevNode { get; set; }
        }
    }
}