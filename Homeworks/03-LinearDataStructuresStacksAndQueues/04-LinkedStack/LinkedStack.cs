using System;

public class LinkedStack<T>
{
    private class Node<T>
    {
        public Node(T value, Node<T> nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public T Value { get; private set; }

        public Node<T> NextNode { get; set; }
    }

    private Node<T> firstNode;

    public int Count { get; private set; }

    public void Push(T element)
    {
        var node = new Node<T>(element);
        if (this.firstNode == null)
        {
            this.firstNode = node;
        }
        else
        {
            node.NextNode = this.firstNode;
            this.firstNode = node;
        }

        this.Count++;
    }

    public T Pop()
    {
        if (this.Count<=0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        var nodeToReturn = this.firstNode;
        this.firstNode = this.firstNode.NextNode;
        this.Count--;

        return nodeToReturn.Value;
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        var node = this.firstNode;
        int index = 0;

        while (node != null)
        {
            array[index] = node.Value;
            node = node.NextNode;
            index++;
        }

        return array;
    }

    private void Grow() { }
}
