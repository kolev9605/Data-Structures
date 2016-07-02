namespace _01_HashTableWithChaining
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Wintellect.PowerCollections;

    class DatesInRange
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var events = new OrderedMultiDictionary<DateTime, string>(true);
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string eventEntry = Console.ReadLine();
                var tokens = eventEntry.Split('|');
                string eventName = tokens[0].Trim();
                DateTime eventDate = DateTime.Parse(tokens[1].Trim());
                events.Add(eventDate, eventName);
            }

            int numberTests = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberTests; i++)
            {
                string[] input = Console.ReadLine().Split('|');
                DateTime from = DateTime.Parse(input[0].Trim());
                DateTime to = DateTime.Parse(input[1].Trim());

                var eventsInRange = events.Range(from, true, to, true);

                Console.WriteLine(eventsInRange.KeyValuePairs.Count);
                foreach (var e in eventsInRange)
                {
                    foreach (var date in e.Value)
                    {
                        Console.WriteLine("{0} | {1:dd-MMM-yyyy}", date, e.Key);
                    }
                }
            }
        }
    }
}
