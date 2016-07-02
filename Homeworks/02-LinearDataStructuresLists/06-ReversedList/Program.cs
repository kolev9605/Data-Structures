namespace _06_ReversedList
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ReversedList<int> list = new ReversedList<int>(2);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(0);

            Console.WriteLine("Foreach with reversed indexes");
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(list.Count);
        }
    }
}