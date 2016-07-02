namespace _06_ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 16;
        private int capacity;
        private int currentIndex;

        private T[] array;

        public ReversedList()
        {
            this.Capacity = DefaultCapacity;
            this.Count = 0;
            this.array = new T[this.Capacity];
            this.currentIndex = 0;
        }

        public ReversedList(int capacity)
        {
            this.Capacity = capacity;
            this.Count = 0;
            this.array = new T[this.Capacity];
            this.currentIndex = 0;
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The capacity cannot be negative number or zero");
                }

                this.capacity = value;
            }
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                return this.array[this.GetItemByIndexReversed(index)];
            }

            set
            {
                this.EnsureIndexInRange(index);
                this.array[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.HasToBeResized())
            {
                this.Resize();
            }
            
            this.array[this.currentIndex] = item;
            this.currentIndex++;
            this.Count++;
        }

        public void Remove(int index)
        {
            this.EnsureIndexInRange(index);

            T[] newArray = new T[this.Count - 1];
            for (int i = 0; i < this.Count - index - 1; i++)
            {
                newArray[i] = this.array[i];
            }

            for (int i = this.Count - index; i < this.Count; i++)
            {
                newArray[i - 1] = this.array[i];
            }

            this.array = newArray;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[this.GetItemByIndexReversed(i)];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureIndexInRange(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index was out of the acceptable boundaries.");
            }
        }

        private bool HasToBeResized()
        {
            if (this.Count == this.Capacity)
            {
                return true;
            }

            return false;
        }

        private void Resize()
        {
            this.Capacity *= 2;
            T[] newArray = new T[this.Capacity];

            Array.Copy(this.array, newArray, this.array.Length);
            this.array = newArray;
        }

        private int GetItemByIndexReversed(int index)
        {
            return this.Count - index - 1;
        }
    }
}
