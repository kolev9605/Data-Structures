namespace _02_BiDictionary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BiDictionary<K1, K2, T>
    {
        private Dictionary<K1, List<T>> valuesByFirstKey;
        private Dictionary<K2, List<T>> valuesBySecondKey;
        private Dictionary<Tuple<K1, K2>, List<T>> valuesByBothKeys;

        public BiDictionary()
        {
            this.valuesByFirstKey = new Dictionary<K1, List<T>>();
            this.valuesBySecondKey = new Dictionary<K2, List<T>>();
            this.valuesByBothKeys = new Dictionary<Tuple<K1, K2>, List<T>>();
        }

        public void Add(K1 key1, K2 key2, T value)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                this.valuesByFirstKey[key1] = new List<T>();
            }

            this.valuesByFirstKey[key1].Add(value);

            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                this.valuesBySecondKey[key2] = new List<T>();
            }

            this.valuesBySecondKey[key2].Add(value);

            if (!this.valuesByBothKeys.ContainsKey(Tuple.Create(key1, key2)))
            {
                this.valuesByBothKeys[Tuple.Create(key1, key2)] = new List<T>();
            }

            this.valuesByBothKeys[Tuple.Create(key1, key2)].Add(value);
        }

        public IEnumerable<T> Find(K1 key1, K2 key2)
        {
            if (!this.valuesByBothKeys.ContainsKey(Tuple.Create(key1,key2)))
            {
                return Enumerable.Empty<T>();
            }

            return this.valuesByBothKeys[Tuple.Create(key1, key2)];
        }

        public IEnumerable<T> FindByKey1(K1 key1)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                return Enumerable.Empty<T>();
            }

            return this.valuesByFirstKey[key1];
        }

        public IEnumerable<T> FindByKey2(K2 key2)
        {
            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                return Enumerable.Empty<T>();
            }

            return this.valuesBySecondKey[key2];
        }

        public bool Remove(K1 key1, K2 key2)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                return false;
            }

            this.valuesByFirstKey.Remove(key1);

            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                return false;
            }

            this.valuesBySecondKey.Remove(key2);

            if (!this.valuesByBothKeys.ContainsKey(Tuple.Create(key1,key2)))
            {
                return false;
            }

            this.valuesByBothKeys.Remove(Tuple.Create(key1, key2));
            return true;
        }
    }

}
