namespace _02_StringEditor
{
    using System;
    using Wintellect.PowerCollections;

    class StringEditor
    {
        static void Main()
        {
            BigList<char> resultText = new BigList<char>();

            while (true)
            {
                string[] commandToken = Console.ReadLine().Split();
                string command = commandToken[0];

                switch (command)
                {
                    case "INSERT":
                        ExecuteInsert(commandToken, resultText);
                        break;

                    case "APPEND":
                        ExecuteAppend(commandToken, resultText);
                        break;

                    case "DELETE":
                        ExecuteDelete(commandToken, resultText);
                        break;

                    case "PRINT":
                        Console.WriteLine(string.Join("", resultText));
                        break;

                    case "REPLACE":
                        ExecuteReplace(commandToken, resultText);
                        break;

                    case "END":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void ExecuteReplace(string[] commandToken, BigList<char> resultText)
        {
            int startIndex = int.Parse(commandToken[1]);
            int count = int.Parse(commandToken[2]);
            string substringToReplace = commandToken[3];

            if (ValidateRange(startIndex, count, resultText))
            {
                resultText.RemoveRange(startIndex, count);
                resultText.InsertRange(startIndex, substringToReplace);
            }
            else
            {
                Console.WriteLine("ERROR");
            }
        }

        private static void ExecuteDelete(string[] commandToken, BigList<char> resultText)
        {
            int startIndex = int.Parse(commandToken[1]);
            int count = int.Parse(commandToken[2]);
            if (ValidateRange(startIndex, count, resultText))
            {
                resultText.RemoveRange(startIndex, count);
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("ERROR");
            }
        }
        
        private static void ExecuteAppend(string[] commandToken, BigList<char> resultText)
        {
            string toAppend = string.Join(" ", commandToken, 1, commandToken.Length - 1);
            resultText.AddRange(toAppend);
        }

        private static void ExecuteInsert(string[] commandToken, BigList<char> resultText)
        {
            int index = int.Parse(commandToken[1]);
            string toInsert = commandToken[2];
            if (ValidatePosition(index, resultText))
            {
                resultText.InsertRange(index, toInsert);
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("ERROR");
            }
        }

        private static bool ValidatePosition(int pos, BigList<char> text)
        {
            if (pos < 0 || pos >= text.Count)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateRange(int startIndex, int count, BigList<char> resultText)
        {
            if (startIndex < 0)
            {
                return false;
            }

            if (count < 0)
            {
                return false;
            }

            if (startIndex + count >= resultText.Count)
            {
                return false;
            }

            return true;
        }

    }
}
