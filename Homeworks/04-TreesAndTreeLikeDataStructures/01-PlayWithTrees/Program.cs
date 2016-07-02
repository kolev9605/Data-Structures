namespace _01_PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Schema;

    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n - 1; i++)
            {
                int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentValue = values[0];
                int childValue = values[1];
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            int pathSum = int.Parse(Console.ReadLine());
            int subTreeSum = int.Parse(Console.ReadLine());

            var root = FindRoot();

            //Find root
            Console.WriteLine("Root node is: " + root.Value);

            //Find all leaf nodes sorted
            Console.WriteLine(
                "Leaf nodes in increasing order: {0}", 
                string.Join(", ", FindLeafNodes().Select(x => x.Value)));

            //Find all middle nodes sorted
            Console.WriteLine(
                "Middle nodes in increasing order: {0}", 
                string.Join(", ", FindMiddleNodes().Select(x => x.Value)));

            //Find longest path of nodes from root to leaf
            Console.WriteLine("Longest Path:");
            FindLongestPath(root);
            PrintTracedNodeToRoot(longestPathLeaf);

            //Find all paths == desired sum
            Console.WriteLine("Paths of sum {0}:", pathSum);
            FindAllPathsWithSum(root, root.Value, pathSum);

            //Find all subtrees == desired sum
            Console.WriteLine("Subtrees of sum {0}:", subTreeSum);
            FindAllSubtreesWithGivenSum(root, subTreeSum);
        }

        public static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        public static int longestPath;

        public static Tree<int> longestPathLeaf;

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public static Tree<int> FindRoot()
        {
            var node = nodeByValue.Values.FirstOrDefault(x => x.Parent == null);

            return node;
        }

        public static IList<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count > 0)
                .OrderBy(x => x.Value)
                .ToList();

            return middleNodes;
        }

        public static IList<Tree<int>> FindLeafNodes()
        {
            var leafNodes = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count == 0)
                .OrderBy(x => x.Value)
                .ToList();

            return leafNodes;
        }

        public static void FindLongestPath(Tree<int> node, int depth = 1)
        {
            if (depth > longestPath)
            {
                longestPath = depth;
                longestPathLeaf = node;
            }

            foreach (var child in node.Children)
            {
                FindLongestPath(child, depth + 1);
            }
        }

        public static void PrintTracedNodeToRoot(Tree<int> node)
        {
            var list = new List<int>();

            while (node != null)
            {
                list.Add(node.Value);
                node = node.Parent;
            }

            list.Reverse();
            Console.WriteLine(string.Join(" -> ", list) + " (lenght = {0})", list.Count);
        }

        public static void FindAllPathsWithSum(Tree<int> tree, int sum, int desiredSum)
        {
            if (sum == desiredSum)
            {
                PrintTracedNodeToRoot(tree);
            }

            foreach (var child in tree.Children)
            {
                FindAllPathsWithSum(child, sum + child.Value, desiredSum);
            }
        }

        public static void FindAllSubtreesWithGivenSum(Tree<int> tree, int desiredSum)
        {
            List<int> values = new List<int>();
            SumGivenSubtree(tree, values);
            int currentSum = values.Sum();

            if (currentSum == desiredSum)
            {
                Console.WriteLine(string.Join(" + ", values));
            }

            foreach (var child in tree.Children)
            {
                FindAllSubtreesWithGivenSum(child, desiredSum);
            }
        }

        public static void SumGivenSubtree(Tree<int> node, IList<int> values)
        {
            values.Add(node.Value);

            foreach (var child in node.Children)
            {
                SumGivenSubtree(child, values);
            }
        }
    }
}