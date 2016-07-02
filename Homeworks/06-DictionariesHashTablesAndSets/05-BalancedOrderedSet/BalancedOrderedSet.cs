namespace _05_BalancedOrderedSet
{
    using System;

    public class BalancedOrderedSet<T> where T:IComparable<T>
    {
        private AvlTree<T> tree;

        public BalancedOrderedSet()
        {
            this.tree = new AvlTree<T>();
        }

        public int Count
        {
            get { return this.tree.Count; }
        }

        public void Add(T item)
        {
            this.tree.Add(item);
        }

        public bool Contains(T element)
        {
            return this.tree.Contains(element);
        }

        public void Remove(T element)
        {
            //TODO
        }
    }
}
