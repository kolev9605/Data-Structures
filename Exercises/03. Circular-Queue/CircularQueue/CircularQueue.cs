using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 16;

    private T[] elements;
    private int startIndex;
    private int endIndex;
    private int capacity;

    public int Capacity
    {
        get
        {
            return this.capacity;
        }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Capacity cannot be negative number or 0");
            }

            this.capacity = value;
        }
    }

    public int Count { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
        this.Capacity = capacity;
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.Capacity)
        {
            this.Resize();
        }

        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.Capacity;
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException("The queue is empty");
        }

        var elementToReturn = this.elements[this.startIndex];
        this.elements[this.startIndex] = default(T);
        this.startIndex = (this.startIndex + 1) % this.Capacity;
        this.Count--;

        return elementToReturn;
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        this.CopyElements(array);
        return array;
    }

    private void Resize()
    {
        var newElements = new T[this.Capacity * 2];
        this.CopyElements(newElements);
        this.elements = newElements;
        this.startIndex = 0;
        this.endIndex = this.Count;
        this.Capacity *= 2;
    }

    private void CopyElements(T[] array)
    {
        int sourceIndex = this.startIndex;
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.Capacity;
        }
    }
}

class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
