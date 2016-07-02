namespace _02_Rope
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    class Program
    {
        static void Main(string[] args)
        {
            BigList<char> text = new BigList<char>();

            while (true)
            {
                string[] commandToken = Console.ReadLine().Split();
                string command = commandToken[0];
                if (command == "PRINT")
                {
                    Console.WriteLine(text);
                    break;
                }

                string secondToken = commandToken[1];

                switch (command)
                {
                    case "INSERT":
                        text.AddRangeToFront(secondToken);
                        break;

                    case "APPEND":
                        text.AddRange(secondToken);
                        break;

                    case "DELETE":
                        int startIndex = int.Parse(secondToken);
                        int count = int.Parse(commandToken[2]);
                        text.RemoveRange(startIndex, count);
                        break;
                }
            }
        }
    }
}
