namespace _06_SequenceNM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            int[] items = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = items[0];
            int m = items[1];

            if (n > m)
            {
                Console.WriteLine("No solution");
                return;
            }

            Queue<Item<int>> queue = new Queue<Item<int>>();
            queue.Enqueue(new Item<int>(n, null));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.Value == m)
                {
                    PrintSolution(current);
                    break;
                }

                if (current.Value < m)
                {
                    queue.Enqueue(new Item<int>(current.Value * 2, current));
                    queue.Enqueue(new Item<int>(current.Value + 2, current));
                    queue.Enqueue(new Item<int>(current.Value + 1, current));
                }
            }
        }

        private static void PrintSolution(Item<int> current)
        {
            var stack = new Stack<int>();

            while (current != null)
            {
                stack.Push(current.Value);
                current = current.PrevItem;
            }

            Console.WriteLine(string.Join(" -> ", stack));
        }
    }

    class Item<T>
    {
        public Item(T value, Item<T> prevItem)
        {
            this.Value = value;
            this.PrevItem = prevItem;
        }

        public T Value { get; set; }

        public Item<T> PrevItem { get; set; }
    }
}
