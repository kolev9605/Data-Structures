namespace _04_RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            IList<int> list = Console.ReadLine().Split().Select(int.Parse).ToList();

            IList<int> result = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                int occurrences = 1;

                for (int j = 0; j < list.Count; j++)
                {
                    if (i != j && list[i] == list[j])
                    {
                        occurrences++;
                    }
                }

                if (occurrences % 2 == 0)
                {
                    result.Add(list[i]);
                }
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
