namespace _04_OrderedSet
{
    using System;

    class OrderedSetTest
    {
        static void Main(string[] args)
        {
            OrderedSet<int> set = new OrderedSet<int>();

            set.Add(17);
            set.Add(9);
            set.Add(6);
            set.Add(12);
            set.Add(19);
            set.Add(25);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
