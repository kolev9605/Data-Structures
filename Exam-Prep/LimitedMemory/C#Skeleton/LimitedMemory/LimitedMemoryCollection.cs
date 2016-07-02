using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, LinkedListNode<Pair<K, V>>> elements;
        private LinkedList<Pair<K, V>> priority;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.elements = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
            this.priority = new LinkedList<Pair<K, V>>();
        }

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            foreach (var pair in this.priority.Reverse())
            {
                yield return pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count
        {
            get { return this.elements.Count; }
        }

        public void Set(K key, V value)
        {
            Pair<K, V> pair = new Pair<K, V>(key, value);

            if (this.elements.Count >= this.Capacity && !this.elements.ContainsKey(key))
            {
                this.MakeRoom();
            }

            if (!this.elements.ContainsKey(key))
            {
                var node = this.priority.AddLast(pair);
                this.elements[key] = node;
            }
            else
            {
                var node = this.elements[key];
                this.priority.Remove(node);
                var last = this.priority.AddLast(pair);
                this.elements[key] = last;
            }
        }

        public V Get(K key)
        {
            if (!this.elements.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            LinkedListNode<Pair<K, V>> node = this.elements[key];
            this.priority.Remove(node);
            this.priority.AddLast(node);

            return node.Value.Value;
        }

        private void MakeRoom()
        {
            Pair<K, V> lowestPriority = this.priority.First();
            this.priority.RemoveFirst();
            this.elements.Remove(lowestPriority.Key);
        }
    }
}
