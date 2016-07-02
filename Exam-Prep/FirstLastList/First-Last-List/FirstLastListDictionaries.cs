namespace First_Last_List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FirstLastListKur<T> : IFirstLastList<T>
        where T : IComparable<T>
    {
        private Dictionary<T, LinkedList<T>> elements; 
        private SortedDictionary<T, LinkedList<T>> sortedElements;

        public FirstLastListKur()
        {
            this.elements = new Dictionary<T, LinkedList<T>>();
            this.sortedElements = new SortedDictionary<T, LinkedList<T>>();
        }

        public void Add(T newElement)
        {
            if (!this.elements.ContainsKey(newElement))
            {
                this.elements[newElement] = new LinkedList<T>();
            }

            this.elements[newElement].AddLast(newElement);

            if (!this.sortedElements.ContainsKey(newElement))
            {
                this.sortedElements[newElement] = new LinkedList<T>();
            }

            this.sortedElements[newElement].AddLast(newElement);
        }

        public int Count
        {
            get { return this.elements.SelectMany(x => x.Value).Count(); }
        }

        public IEnumerable<T> First(int count)
        {
            this.ValidateCount(count);
            var result = this.elements
                .SelectMany(x => x.Value)
                .Take(count);

            return result;
        }

        public IEnumerable<T> Last(int count)
        {
            this.ValidateCount(count);
            var result = this.elements
                .Reverse()
                .SelectMany(x => x.Value)
                .Take(count);

            return result;
        }

        public IEnumerable<T> Min(int count)
        {
            this.ValidateCount(count);
            var result = this.sortedElements
                .SelectMany(x => x.Value)
                .Take(count);

            return result;
        }

        public IEnumerable<T> Max(int count)
        {
            this.ValidateCount(count);
            var result = this.sortedElements
                .Reverse()
                .SelectMany(x => x.Value)
                .Take(count);

            return result;
        }

        public int RemoveAll(T element)
        {
            if (!this.elements.ContainsKey(element))
            {
                return 0;
            }

            int removedElements = this.elements[element].Count;
            this.elements.Remove(element);
            this.sortedElements.Remove(element);

            return removedElements;
        }

        public void Clear()
        {
            this.elements.Clear();
            this.sortedElements.Clear();
        }

        private void ValidateCount(int count)
        {
            if (count > this.elements.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
