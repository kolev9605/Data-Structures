namespace _02_CalculateSequenceQueue
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Queue<int> queue = new Queue<int>();
            var result = new List<int>();
            queue.Enqueue(n);

            while (result.Count < 50)
            {
                int currentElement = queue.Dequeue();
                result.Add(currentElement);
                queue.Enqueue(currentElement + 1);
                queue.Enqueue(2 * currentElement + 1);
                queue.Enqueue(currentElement + 2);
            }

            Console.WriteLine(string.Join(", ", result));
        }
    }
}
