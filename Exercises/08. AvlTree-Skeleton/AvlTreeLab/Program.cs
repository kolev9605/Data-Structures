namespace AvlTreeLab
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            AvlTree<int> tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(50);
            tree.Add(10);
            tree.Add(25);
            tree.Add(40);
            tree.Add(70);
            tree.Add(5);
            tree.Add(15);
            tree.Add(45);
            tree.Add(60);

            tree.ForeachDfs((x,y) =>  Console.WriteLine(new string(' ', 2 * x) + y));
        }
    }
}
