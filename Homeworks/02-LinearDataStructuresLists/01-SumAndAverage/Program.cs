namespace _01_SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            if (input != String.Empty)
            {
                IList<int> list = input.Split().Select(int.Parse).ToList();
                Console.WriteLine("Sum = {0}, Average = {1}", list.Sum(), list.Average());
            }
            else
            {
                Console.WriteLine("Sum = {0}, Average = {1}", 0, 0);
            }
        }
    }
}
