namespace _03_ArrayBasedStack
{
    using System;

    public class ArrayStack<T>
    {
        private const int DefaultCapacity = 16;
        private T[] elements;
        private int lastIndex;
        private int capacity;

        public ArrayStack(int capacity = DefaultCapacity)
        {
            this.elements = new T[capacity];
            this.Capacity = capacity;
        }

        public int Count { get; private set; }

        public int Capacity { get; private set; }

        public void Push(T element)
        {
            if (this.Count == this.Capacity)
            {
                this.Grow();
            }

            this.elements[this.lastIndex] = element;
            this.lastIndex++;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            var elementToReturn = this.elements[this.lastIndex - 1];
            this.elements[this.lastIndex - 1] = default(T);
            this.lastIndex--;
            this.Count--;

            return elementToReturn;
        }

        public T[] ToArray()
        {
            var result = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                result[i] = this.elements[i];
            }

            Array.Reverse(result);
            return result;
        }

        private void Grow()
        {
            T[] array = new T[this.Capacity * 2];
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = this.elements[i];
            }

            this.elements = array;
            this.Capacity *= 2;
        }
    }
}
