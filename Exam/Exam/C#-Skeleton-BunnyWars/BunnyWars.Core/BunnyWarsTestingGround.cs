namespace BunnyWars.Core
{
    using System;
    using Wintellect.PowerCollections;

    class BunnyWarsTestingGround
    {
        static void Main(string[] args)
        {
            OrderedSet<int> set1 = new OrderedSet<int>();
            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);

            OrderedSet<int> set2 = new OrderedSet<int>();

            set2.Add(4);
            set2.Add(5);
            set2.Add(6);
            set2.Add(6);

            Console.WriteLine(set1.Difference(set2));
        }
    }
}
