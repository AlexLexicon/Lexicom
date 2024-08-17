namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public class TestRandomProvider : IRandomProvider
{
    protected readonly Queue<int> _nexts;
    protected readonly Queue<long> _nextInt64s;
    protected readonly Queue<float> _nextSingles;
    protected readonly Queue<double> _nextDoubles;

    public TestRandomProvider()
    {
        RandomProvider = new RandomProvider();

        _nexts = new Queue<int>();
        _nextInt64s = new Queue<long>();
        _nextSingles = new Queue<float>();
        _nextDoubles = new Queue<double>();
    }
    public TestRandomProvider(int seed)
    {
        RandomProvider = new RandomProvider(seed);

        _nexts = new Queue<int>();
        _nextInt64s = new Queue<long>();
        _nextSingles = new Queue<float>();
        _nextDoubles = new Queue<double>();
    }

    private RandomProvider RandomProvider { get; set; }

    public virtual int Next()
    {
        if (_nexts.TryDequeue(out int next))
        {
            return next;
        }

        return RandomProvider.Next();
    }
    public virtual int Next(int maxValue)
    {
        if (_nexts.TryDequeue(out int next))
        {
            return next;
        }

        return RandomProvider.Next(maxValue);
    }
    public virtual int Next(int minValue, int maxValue)
    {
        if (_nexts.TryDequeue(out int next))
        {
            return next;
        }

        return RandomProvider.Next(minValue, maxValue);
    }

    public virtual long NextInt64()
    {
        if (_nextInt64s.TryDequeue(out long nextInt64))
        {
            return nextInt64;
        }

        return RandomProvider.NextInt64();
    }
    public virtual long NextInt64(long maxValue)
    {
        if (_nextInt64s.TryDequeue(out long nextInt64))
        {
            return nextInt64;
        }

        return RandomProvider.NextInt64(maxValue);
    }
    public virtual long NextInt64(long minValue, long maxValue)
    {
        if (_nextInt64s.TryDequeue(out long nextInt64))
        {
            return nextInt64;
        }

        return RandomProvider.NextInt64(minValue, maxValue);
    }

    public virtual float NextSingle()
    {
        if (_nextSingles.TryDequeue(out float nextSingle))
        {
            return nextSingle;
        }

        return RandomProvider.NextSingle();
    }

    public virtual double NextDouble()
    {
        if (_nextDoubles.TryDequeue(out double nextDouble))
        {
            return nextDouble;
        }

        return RandomProvider.NextDouble();
    }
    public virtual double NextDouble(double maxValue)
    {
        if (_nextDoubles.TryDequeue(out double nextDouble))
        {
            return nextDouble;
        }

        return RandomProvider.NextDouble(maxValue);
    }
    public virtual double NextDouble(double minValue, double maxValue)
    {
        if (_nextDoubles.TryDequeue(out double nextDouble))
        {
            return nextDouble;
        }

        return RandomProvider.NextDouble(minValue, maxValue);
    }

    public virtual void NextBytes(byte[] buffer) => RandomProvider.NextBytes(buffer);
    public virtual void NextBytes(Span<byte> buffer) => RandomProvider.NextBytes(buffer);

    public virtual void Set(int next)
    {
        _nexts.Clear();

        Enqueue(next);
    }
    public virtual void Set(long next64)
    {
        _nextInt64s.Clear();

        Enqueue(next64);
    }
    public virtual void Set(float nextSingle)
    {
        _nextSingles.Clear();

        Enqueue(nextSingle);
    }
    public virtual void Set(double nextDouble)
    {
        _nextDoubles.Clear();

        Enqueue(nextDouble);
    }

    public virtual void Enqueue(int next)
    {
        _nexts.Enqueue(next);
    }
    public virtual void Enqueue(long next64)
    {
        _nextInt64s.Enqueue(next64);
    }
    public virtual void Enqueue(float nextSingle)
    {
        _nextSingles.Enqueue(nextSingle);
    }
    public virtual void Enqueue(double nextDouble)
    {
        _nextDoubles.Enqueue(nextDouble);
    }

    public virtual void Seed(int seed)
    {
        RandomProvider = new RandomProvider(seed);
    }
}
