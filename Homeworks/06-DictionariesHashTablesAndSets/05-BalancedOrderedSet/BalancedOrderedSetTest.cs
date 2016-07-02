namespace _05_BalancedOrderedSet
{
    using System;

    class BalancedOrderedSetTest
    {
        static void Main(string[] args)
        {
            BalancedOrderedSet<int> set = new BalancedOrderedSet<int>();

            set.Add(10);
            set.Add(9);
            set.Add(8);
            set.Add(7);
            set.Add(6);
            set.Add(5);
            set.Add(4);
            set.Add(3);

            Console.WriteLine();
        }
    }
}
