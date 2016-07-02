namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Dictionary<T, Element<T>> hierarchy;

        private T root;

        public Hierarchy(T root)
        {
            this.hierarchy = new Dictionary<T, Element<T>>();
            this.hierarchy[root] = new Element<T>();
            this.root = root;
        }

        public int Count
        {
            get { return this.hierarchy.Count; }
        }

        public void Add(T element, T child)
        {
            if (!this.hierarchy.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.hierarchy.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            this.hierarchy[child] = new Element<T>();
            this.hierarchy[child].Parent = element;
            this.hierarchy[element].Children[child] = child;
        }

        public void Remove(T element)
        {
            if (!this.hierarchy.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.hierarchy[element].Parent.Equals(default(T)))
            {
                throw new InvalidOperationException();
            }

            if (this.hierarchy[element].Children.Count != 0)
            {
                foreach (var child in this.hierarchy[element].Children)
                {
                    this.hierarchy[this.hierarchy[element].Parent].Children[child.Key] = child.Key;
                    this.hierarchy[child.Key].Parent = this.hierarchy[element].Parent;
                }
            }

            this.hierarchy[this.hierarchy[element].Parent].Children.Remove(element);
            this.hierarchy.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.hierarchy.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var children = this.hierarchy[item].Children.Keys;

            return children;
        }

        public T GetParent(T item)
        {
            if (!this.hierarchy.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            return this.hierarchy[item].Parent;
        }

        public bool Contains(T value)
        {
            return this.hierarchy.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var thisChildren = this.hierarchy.Keys;
            var otherChildren = other.hierarchy.Keys;

            return thisChildren.Intersect(otherChildren);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<T> queue = new Queue<T>();
            List<T> output = new List<T>();

            queue.Enqueue(this.root);
            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                output.Add(currentNode);

                foreach (var child in this.hierarchy[currentNode].Children)
                {
                    queue.Enqueue(child.Key);
                }
            }

            foreach (var item in output)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}