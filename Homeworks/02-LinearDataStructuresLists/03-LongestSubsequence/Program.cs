namespace _03_LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            IList<int> list = Console.ReadLine().Split().Select(int.Parse).ToList();
            var result = LongestSubsequence(list);
            Console.WriteLine(string.Join(" ", result));
        }

        public static IList<int> LongestSubsequence(IList<int> list)
        {
            int currentMaxCount = 1;
            int startIndex = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                int currentCount = 1;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] == list[j])
                    {
                        currentCount++;
                    }
                }

                if (currentCount > currentMaxCount)
                {
                    currentMaxCount = currentCount;
                    startIndex = i;
                }
            }

            var result = new List<int>();
            for (int i = 0; i < currentMaxCount; i++)
            {
                result.Add(list[startIndex]);
                startIndex++;
            }

            return result;
        }
    }
}
