namespace _01_AvlTreeImplementation
{
    using System;
    using AvlTreeLab;

    public class AvlTree<T> where T : IComparable<T>
    {
        private Node<T> root;

        public int Count { get; private set; }

        public Node<T> Root => this.root;

        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                var node = new Node<T>(item);
                this.root = node;
            }
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        private bool InsertInternal(Node<T> node, T item)
        {
            var currentNode = node;
            var newNode = new Node<T>(item);
            var shouldRetrace = false;
            while (true)
            {
                if (currentNode.Value.CompareTo(item) < 0) //node value is less than the item (item>currentNode.Value) -> go right
                {
                    if (currentNode.RightChild == null)
                    {
                        //set the node as right child, its empty
                        currentNode.RightChild = newNode;
                        currentNode.BalanceFactor--;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else if (currentNode.Value.CompareTo(item) > 0)//node value is greater than the item (item<currentNode.Value) -> go left
                {
                    if (currentNode.LeftChild == null)
                    {
                        //set the node as left child, its empty
                        currentNode.LeftChild = newNode;
                        currentNode.BalanceFactor++;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    break;
                }
            }

            if (shouldRetrace)
            {
                this.RetraceInsert(currentNode);
            }

            return true;
        }

        private void RetraceInsert(Node<T> node)
        {
            var parent = node.Parent;
            while (parent != null)
            {
                //node is left -> parent's left subtree has grown
                if (node.IsLeftChild)
                {
                    if (parent.BalanceFactor == 1)
                    {
                        //after the adding the parent's balance factor will become 2, rotation is needed
                        parent.BalanceFactor++;
                        if (node.BalanceFactor == -1)
                        {
                            //this means that the current node has right child, so we need to make the branch straight
                            this.RotateLeft(node);
                        }

                        //now after the branch is straight we can perform the final rotation to balance the branch
                        this.RotateRight(parent);
                        break;
                    }
                    else if (parent.BalanceFactor == -1)
                    {
                        //after the adding the parent is now balanced, no need to rotate
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        //the branch was balanced before adding, now its BF is 1, which is fine for the AVL tree
                        parent.BalanceFactor = 1;
                    }
                }
                else
                {
                    //node is right -> parent's right subtree has grown
                    if (parent.BalanceFactor == -1)
                    {
                        //after the adding the parent's balance factor will become -2, rotation is needed
                        parent.BalanceFactor--;
                        if (node.BalanceFactor == 1)
                        {
                            //this means that the current node has left child, so we need to make the branch straight
                            this.RotateRight(node);
                        }

                        //now after the branch is straight we can perform the final rotation to balance the branch
                        this.RotateLeft(parent);
                        break;
                    }
                    else if (parent.BalanceFactor == 1)
                    {
                        //after the adding the parent is now balanced, no need to rotate
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        //the branch was balanced before adding, now its BF is -1, which is fine for the AVL tree
                        parent.BalanceFactor = -1;
                    }
                }

                node = parent;
                parent = node.Parent;
            }
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.RightChild;
            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.LeftChild;

            if (parent != null)
            {
                if (node.IsRightChild)
                {
                    parent.RightChild = child;
                }
                else
                {
                    parent.LeftChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BalanceFactor -= 1 + Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 - Math.Min(node.BalanceFactor, 0);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.CompareTo(item) == 0)
                {
                    return true;
                }
                else if (node.Value.CompareTo(item) < 0)
                {
                    node = node.RightChild;
                }
                else if (node.Value.CompareTo(item) > 0)
                {
                    node = node.LeftChild;
                }
            }

            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }

            this.InOrderDfs(this.root, 1, action);
        }

        private void InOrderDfs(Node<T> node, int depth, Action<int, T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, depth + 1, action);
            }

            action(depth, node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, depth + 1, action);
            }
        }
    }
}
