namespace _02.SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            IList<string> list = Console.ReadLine().Split().ToList();
            Console.WriteLine(string.Join(", ", list.OrderBy(x => x)));
        }
    }
}