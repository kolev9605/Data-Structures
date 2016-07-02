namespace _03_Phonebook
{
    using System;
    using _01_HashTable;

    class Phonebook
    {
        static void Main()
        {
            HashTable<string, string> phonebook = new HashTable<string, string>();

            string input = Console.ReadLine();
            while (input != "search")
            {
                var tokens = input.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                var name = tokens[0];
                var number = tokens[1];

                phonebook[name] = number;

                input = Console.ReadLine();
            }

            input = Console.ReadLine();
            while (input != null)
            {
                var entry = phonebook.Find(input);
                if (entry != null)
                {
                    Console.WriteLine("{0} -> {1}", entry.Key, entry.Value);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", input);
                }

                input = Console.ReadLine();
            }
        }
    }
}
