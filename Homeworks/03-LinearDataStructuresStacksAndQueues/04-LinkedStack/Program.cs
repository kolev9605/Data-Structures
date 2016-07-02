using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_LinkedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> namaikati = new Stack<int>();

            namaikati.Push(5);
            namaikati.Push(4);
            namaikati.Push(3);
            namaikati.Push(2);
            namaikati.Push(1);
            namaikati.Push(0);

            Console.WriteLine(string.Join(", ",namaikati.ToArray()));
        }
    }
}
