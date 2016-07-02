namespace AvlTreeLab
{
    using System;

    public class Node<T> where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild;

        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }
            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }
            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Parent { get; set; }

        public int BalanceFactor { get; set; }

        public bool IsLeftChild
        {
            get
            {
                if (this.Parent == null)
                {
                    return false;
                }

                if (this.Parent.LeftChild == this)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsRightChild
        {
            get
            {
                if (this.Parent == null)
                {
                    return false;
                }

                if (this.Parent.RightChild == this)
                {
                    return true;
                }

                return false;
            }
        }

        public int ChildrenCount
        {
            get
            {
                int count = 0;
                if (this.LeftChild != null)
                {
                    count++;
                }

                if (this.RightChild != null)
                {
                    count++;
                }

                return count;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}