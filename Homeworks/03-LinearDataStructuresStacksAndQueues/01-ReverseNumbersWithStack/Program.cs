namespace _01_ReverseNumbersWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            var arr = input.Split().Select(int.Parse).ToArray();
            Stack<int> stack = new Stack<int>();
            foreach (var element in arr)
            {
                stack.Push(element);
            }

            while (stack.Count > 0)
            {
                Console.Write(stack.Pop() + " ");
            }

            Console.WriteLine();
        }
    }
}
