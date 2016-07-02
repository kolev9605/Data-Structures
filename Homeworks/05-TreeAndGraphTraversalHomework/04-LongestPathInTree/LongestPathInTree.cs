namespace _04_LongestPathInTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LongestPathInTree
    {
        private static Dictionary<int, Node> nodes;
        private static long currentMaxSum;

        static void Main()
        {
            ReadNodes();
            var root = GetRoot();
            FindBiggestSum(root);

            Console.WriteLine(currentMaxSum);
        }

        private static void FindBiggestSum(Node node)
        {
            foreach (var child in node.Children)
            {
                FindBiggestSum(child);
            }

            currentMaxSum = node.NodeSum;
        }

        private static Node GetRoot()
        {
            return nodes.FirstOrDefault(x => x.Value.Parent == null).Value;
        }

        private static void ReadNodes()
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            nodes = new Dictionary<int, Node>();

            for (int i = 0; i < numberOfEdges; i++)
            {
                var pairNodes = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var parent = GetNode(pairNodes[0]);
                var child = GetNode(pairNodes[1]);
                parent.Children.Add(child);
                child.Parent = parent;
            }
        }

        private static Node GetNode(int value)
        {
            if (!nodes.ContainsKey(value))
            {
                nodes[value] = new Node(value);
            }

            return nodes[value];
        }
    }
}
