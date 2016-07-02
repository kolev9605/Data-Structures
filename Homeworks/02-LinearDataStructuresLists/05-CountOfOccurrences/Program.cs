namespace _05_CountOfOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            int[] inputArray = Console.ReadLine().Split().Select(int.Parse).ToArray();

            IDictionary<int, int> occurrences = new Dictionary<int, int>();

            foreach (var element in inputArray)
            {
                if (!occurrences.ContainsKey(element))
                {
                    occurrences.Add(element, 0);
                }

                occurrences[element]++;
            }

            foreach (var pair in occurrences)
            {
                Console.WriteLine("{0} -> {1} times", pair.Key, pair.Value);
            }
        }
    }
}