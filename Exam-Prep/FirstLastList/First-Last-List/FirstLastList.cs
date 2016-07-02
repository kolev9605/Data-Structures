using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T>
    where T : IComparable<T>
{
    private LinkedList<T> elements;
    private SortedDictionary<T, LinkedList<LinkedListNode<T>>> sortedElements;

    public FirstLastList()
    {
        this.elements = new LinkedList<T>();
        this.sortedElements = new SortedDictionary<T, LinkedList<LinkedListNode<T>>>();
    }

    public void Add(T newElement)
    {
        var node = this.elements.AddLast(newElement);
        if (!this.sortedElements.ContainsKey(newElement))
        {
            this.sortedElements[newElement] = new LinkedList<LinkedListNode<T>>();
        }

        this.sortedElements[newElement].AddLast(node);
    }

    public int Count
    {
        get { return this.elements.Count; }
    }

    public IEnumerable<T> First(int count)
    {
        this.ValidateCount(count);
        var result = this.elements.Take(count);

        return result;
    }

    public IEnumerable<T> Last(int count)
    {
        this.ValidateCount(count);
        var result = this.elements.Reverse().Take(count);

        return result;
    }

    public IEnumerable<T> Min(int count)
    {
        this.ValidateCount(count);
        var result = this.sortedElements
            .SelectMany(x => x.Value)
            .Select(x => x.Value)
            .Take(count);

        return result;
    }

    public IEnumerable<T> Max(int count)
    {
        this.ValidateCount(count);
        var result = this.sortedElements
            .Reverse()
            .SelectMany(x => x.Value)
            .Select(x => x.Value)
            .Take(count);

        return result;
    }

    public int RemoveAll(T element)
    {
        int count = 0;
        if (!this.sortedElements.ContainsKey(element))
        {
            return count;
        }

        foreach (var node in this.sortedElements[element])
        {
            this.elements.Remove(node);
            count++;
        }

        this.sortedElements.Remove(element);
        return count;
    }

    public void Clear()
    {
        this.elements.Clear();
        this.sortedElements.Clear();
    }

    private void ValidateCount(int count)
    {
        if (count > this.elements.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
