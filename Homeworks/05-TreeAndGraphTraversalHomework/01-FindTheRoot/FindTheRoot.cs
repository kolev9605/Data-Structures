namespace _01_FindTheRoot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FindTheRoot
    {
        public static void Main(string[] args)
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            Dictionary<int, List<int>> nodes = new Dictionary<int, List<int>>();

            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes[i] = new List<int>();
            }

            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] pairNodes = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parent = pairNodes[0];
                int child = pairNodes[1];

                nodes[parent].Add(child);
            }

            var roots = new List<int>();

            foreach (var node in nodes)
            {
                var isTree = true;

                foreach (var node1 in nodes)
                {
                    if (node.Key != node1.Key)
                    {
                        if (node1.Value.Contains(node.Key))
                        {
                            isTree = false;
                        }
                    }
                }

                if (isTree)
                {
                    roots.Add(node.Key);
                }
            }

            if (roots.Count == 0)
            {
                Console.WriteLine("No root!");
            }
            else if (roots.Count == 1)
            {
                Console.WriteLine("There is only 1 root: {0}. The structure is tree", roots[0]);
            }
            else
            {
                Console.WriteLine("Miltiple roots: {0}. The structure is graph.", string.Join(" ", roots));
            }
        }
    }
}
