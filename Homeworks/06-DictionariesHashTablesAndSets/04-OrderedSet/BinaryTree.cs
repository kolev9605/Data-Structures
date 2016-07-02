namespace _04_OrderedSet
{
    using System;

    public class BinaryTree<T> where T:IComparable<T>
    {
        public BinaryTree(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public bool IsLeftChild
        {
            get { return this.Parent.LeftChild == this; }
        }

        public bool IsRightChild
        {
            get { return this.Parent.RightChild == this; }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
