using System.Collections.Generic;

public class DynamicPool<T>
{
    private Stack<T> pool;

    public DynamicPool()
    {
        pool = new Stack<T>();
    }

    public void Push(T obj)
    {
        pool.Push(obj);
    }

    public T Pull()
    {
        if (pool.Count > 0)
        {
            return pool.Pop();
        }

        return default;
    }
}