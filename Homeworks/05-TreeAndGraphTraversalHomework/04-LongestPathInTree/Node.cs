namespace _04_LongestPathInTree
{
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        private int sumOfNode;
        private bool hasCalculatedSum;

        public Node(int value)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.hasCalculatedSum = false;
        }

        public int Value { get; set; }

        public IList<Node> Children { get; set; }

        public Node Parent { get; set; }

        public int NodeSum
        {
            get
            {
                if (!this.hasCalculatedSum)
                {
                    this.sumOfNode += this.Value;
                    this.hasCalculatedSum = true;
                }

                if (this.Parent == null)
                {
                    var rootChildren = this.Children.OrderByDescending(x => x.NodeSum).ToList();

                    this.sumOfNode = this.Value + rootChildren[0].NodeSum + rootChildren[1].NodeSum;
                }
                else
                {
                    if (this.Parent.sumOfNode < this.sumOfNode)
                    {
                        this.Parent.sumOfNode = this.sumOfNode;
                    }
                }

                return this.sumOfNode;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
