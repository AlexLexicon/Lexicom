using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class AssemblyScanBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanInital For<TAssignableTo>(this IAssemblyScanBuilder builder, AssemblyScanOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var assemblyScan = new AssemblyScan<TAssignableTo>(builder.AssemblyScanMarker, options ?? AssemblyScanOptions.Default);

        builder.Services.AddSingleton<AssemblyScan>(assemblyScan);

        return assemblyScan;
    }
}
