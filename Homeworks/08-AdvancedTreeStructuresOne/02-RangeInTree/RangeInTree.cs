namespace _02_RangeInTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AvlTreeLab;
    using _01_AvlTreeImplementation;

    class RangeInTree
    {
        static void Main()
        {
            AvlTree<int> tree = new AvlTree<int>();
            int[] elements = Console.ReadLine().Split().Select(int.Parse).ToArray();
            foreach (var element in elements)
            {
                tree.Add(element);
            }

            int[] range = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int from = range[0];
            int to = range[1];

            var elementInRange = FindElementInRange(tree, from, to);

            Console.WriteLine("Elements in range [{0} - {1}] are : {2}", 
                from, 
                to, 
                elementInRange.Count > 0 ? string.Join(", ", elementInRange) : "None");
        }

        private static IList<int> FindElementInRange(AvlTree<int> tree, int from, int to)
        {
            IList<int> itemsInRange = new List<int>();
            var rootNode = tree.Root;
            TraverseTree(rootNode, from, to, itemsInRange);

            return itemsInRange;
        }

        private static void TraverseTree(Node<int> currentNode, int from, int to, IList<int> itemsInRange)
        {
            if (currentNode.LeftChild != null && currentNode.LeftChild.Value >= from &&
                currentNode.LeftChild.Value <= to)
            {
                TraverseTree(currentNode.LeftChild, from, to, itemsInRange);
            }

            if (currentNode.Value >= from && currentNode.Value <= to)
            {
                itemsInRange.Add(currentNode.Value);
            }

            if (currentNode.RightChild != null && currentNode.RightChild.Value >= from && currentNode.RightChild.Value <= to)
            {
                TraverseTree(currentNode.RightChild, from, to, itemsInRange);
            }
        }
    }
}
