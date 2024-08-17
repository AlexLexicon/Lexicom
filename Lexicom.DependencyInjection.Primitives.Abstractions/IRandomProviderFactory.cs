namespace Lexicom.DependencyInjection.Primitives.Abstractions;
public interface IRandomProviderFactory
{
    IRandomProvider Create();
    IRandomProvider Create(int seed);
}
