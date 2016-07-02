namespace _02_CountSymbols
{
    using System;
    using System.Linq;
    using _01_HashTable;

    public class CountSymbols
    {
        private static HashTable<char, int> hashTable;

        public static void Main()
        {
            hashTable = new HashTable<char, int>();
            string input = Console.ReadLine();

            foreach (var character in input)
            {
                AddCharacter(character);
            }

            foreach (var pair in hashTable.OrderBy(x => x.Key))
            {
                Console.WriteLine("{0}: {1} time/s", pair.Key, pair.Value);
            }
        }

        public static void AddCharacter(char key)
        {
            if (!hashTable.ContainsKey(key))
            {
                hashTable[key] = 0;
            }

            hashTable[key]++;
        }
    }
}
