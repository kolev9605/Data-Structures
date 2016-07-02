namespace _02_RoundDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RoundDance
    {
        private static Dictionary<int, List<int>> graph;
        private static Dictionary<int, bool> visited;
        private static IList<IList<int>> allPaths; 

        static void Main()
        {
            graph = new Dictionary<int, List<int>>();
            visited = new Dictionary<int, bool>();
            var root = ReadInputAndReturnRoot();

            var longestPath = new List<int>();
            allPaths = new List<IList<int>>();
            Dfs(root, root, longestPath);

            Console.WriteLine("The round dance with the most people: {0}", allPaths.Max(x => x.Count));
            foreach (var path in allPaths.Where(path => path.Count == allPaths.Max(x => x.Count)))
            {
                Console.WriteLine(string.Join(" ", path));
            }
        }

        private static int ReadInputAndReturnRoot()
        {
            int numberOfEdges = int.Parse(Console.ReadLine());
            int root = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] friendship = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int firstPerson = friendship[0];
                int secondPerson = friendship[1];

                if (!graph.ContainsKey(firstPerson))
                {
                    graph[firstPerson] = new List<int>();
                }

                graph[firstPerson].Add(secondPerson);
                visited[firstPerson] = false;

                if (!graph.ContainsKey(secondPerson))
                {
                    graph[secondPerson] = new List<int>();
                }

                graph[secondPerson].Add(firstPerson);
                visited[secondPerson] = false;
            }

            return root;
        }

        static void Dfs(int rootNode, int currentRoot, IList<int> longestPath)
        {
            foreach (var child in graph[graph.Keys.FirstOrDefault(x => x == currentRoot)])
            {
                visited[currentRoot] = true;
                if (!visited[child])
                {
                    longestPath.Add(child);
                    visited[child] = true;
                    Dfs(rootNode, child, longestPath);
                    if (longestPath.Count > 0)
                    {
                        longestPath.Insert(0, rootNode);
                        allPaths.Add(new List<int>(longestPath));
                        longestPath.Clear();
                    }
                }
            }
        }
    }
}
