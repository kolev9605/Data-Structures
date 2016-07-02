namespace _04_OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTree<T> root;

        public int Count { get; private set; }

        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                var node = new BinaryTree<T>(item);
                this.root = node;
            }
            else
            {
                inserted = this.InsertNode(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        public bool Contains(T element)
        {
            var node = this.LookupForNode(element);
            if (node != null)
            {
                return true;
            }

            return false;
        }

        public void Remove(T element)
        {
            //var node = this.LookupForNode(element);
            //if (node == null)
            //{
            //    throw new InvalidOperationException("Node does not exist");
            //}

            //if (node.LeftChild == null && BinaryTree.RightChild == null)
            //{
            //    if (node.IsLeftChild)
            //    {
            //        node.Parent.LeftChild = null;
            //    }
            //    else
            //    {
            //        node.Parent.RightChild = null;
            //    }
            //}

            //if (node.LeftChild == null && BinaryTree.RightChild != null)
            //{
            //    //TODO
            //}
        }

        private BinaryTree<T> LookupForNode(T value)
        {
            var currentNode = this.root;

            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(value) == 0)
                {
                    return currentNode;
                }
                if (currentNode.Value.CompareTo(value) > 0)
                {
                    //currentNode.Value > value
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(value) < 0)
                {
                    //currentNode.Value < value
                    currentNode = currentNode.RightChild;
                }
            }

            return null;
        }

        private bool InsertNode(BinaryTree<T> node, T item)
        {
            var currentNode = node;
            var newNode = new BinaryTree<T>(item);
            while (true)
            {
                if (currentNode.Value.CompareTo(newNode.Value) > 0)
                {
                    //this.root.Value > node.Value
                    if (currentNode.LeftChild == null)
                    {
                        //insert left
                        currentNode.LeftChild = newNode;
                        newNode.Parent = currentNode;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(newNode.Value) < 0)
                {
                    //this.root.Value < node.Value
                    if (currentNode.RightChild == null)
                    {
                        //insert right
                        currentNode.RightChild = newNode;
                        newNode.Parent = currentNode;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else
                {
                    //this.root.Value == node.Value
                    //duplicates are not allowed -> return false
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<BinaryTree<T>> Traversal(BinaryTree<T> node)
        {
            if (node.LeftChild != null)
            {
                foreach (var leftNode in this.Traversal(node.LeftChild))
                {
                    yield return leftNode;
                }
            }

            yield return node;

            if (node.RightChild != null)
            {
                foreach (var rightNode in this.Traversal(node.RightChild))
                {
                    yield return rightNode;
                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in this.Traversal(this.root))
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
