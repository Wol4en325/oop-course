using System;
using System.Collections.Generic;

namespace ClinicApp.Models;

public class WaitingQueue<T>
{
    private Queue<T> _queue;

    public int Count
    {
        get
        {
            return _queue.Count;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return _queue.Count == 0;
        }
    }

    public WaitingQueue()
    {
        _queue = new Queue<T>();
    }

    public void Enqueue(T item)
    {
        _queue.Enqueue(item);
    }

    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The waiting queue is empty.");
        }

        return _queue.Dequeue();
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The waiting queue is empty.");
        }

        return _queue.Peek();
    }

    public T[] ToArray()
    {
        return _queue.ToArray();
    }
}
