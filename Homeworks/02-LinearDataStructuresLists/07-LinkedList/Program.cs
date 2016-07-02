namespace _07_LinkedList
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            CustomLinkedList<int> list = new CustomLinkedList<int>();

            list.Add(5);
            list.Add(6);
            list.Add(7);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
