namespace SqlBob.Formatting;

/// <summary>A pool for <see cref="SqlBuilder"/> instances that allows re-usage.</summary>
internal class SqlBuilderPool
{
    private readonly int maxReuseSize;
    private readonly SqlBuilder[] pool;
    private readonly object locker = new();
    private int index = -1;

    /// <summary>Creates a new instance of a <see cref="SqlBuilderPool"/>.</summary>
    /// <param name="capacity">
    /// The maximum capacity of the pool.
    /// </param>
    /// <param name="maxReuse">
    /// The maximum size a <see cref="SqlBuilder"/> may have to be reused.
    /// </param>
    public SqlBuilderPool(int capacity = 256, int maxReuse = 64 * 1024 /* 64 kiB */)
    {
        maxReuseSize = maxReuse;
        pool = new SqlBuilder[capacity];
    }

    /// <summary>The number of <see cref="SqlBuilder"/>s currently in the pool.</summary>
    public int Count => index + 1;
    
    /// <summary>The maximum number of <see cref="SqlBuilder"/>s this pool can contain.</summary>
    public int Capacity => pool.Length;

    /// <summary>The (memory) size used by the pool.</summary>
    public long Size => pool.Where(sb => sb != null).Sum(sb => sb.Capacity);

    /// <summary>Pops a <see cref="SqlBuilder"/> from the pool.</summary>
    [Pure]
    public SqlBuilder Pop()
    {
        lock (locker)
        {
            return index < 0 ? new SqlBuilder() : pool[index--];
        }
    }

    /// <summary>Pushes a <see cref="SqlBuilder"/> to the pool.</summary>
    public void Push(SqlBuilder builder)
    {
        if (builder.Capacity < maxReuseSize && index < pool.Length - 1)
        {
            lock (locker)
            {
                if (index < pool.Length - 1)
                {
                    pool[++index] = builder;
                    builder.Clear();
                }
            }
        }
    }

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"Count: {0}, Size: {Size / 1000d:#,##0.0} kB";

    /// <summary>Clears the pool.</summary>
    public void Clear()
    {
        lock (locker)
        {
            index = -1;
            Array.Clear(pool, 0, pool.Length);
        }
    }
}
