using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Amenities;
public interface IAssemblyScanBuilder
{
    Type AssemblyScanMarker { get; }
    IServiceCollection Services { get; }
}
public class AssemblyScanBuilder : IAssemblyScanBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AssemblyScanBuilder(
        Type assemblyScanMarker, 
        IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(assemblyScanMarker);
        ArgumentNullException.ThrowIfNull(services);

        AssemblyScanMarker = assemblyScanMarker;
        Services = services;
    }

    public Type AssemblyScanMarker { get; }
    public IServiceCollection Services { get; }
}
