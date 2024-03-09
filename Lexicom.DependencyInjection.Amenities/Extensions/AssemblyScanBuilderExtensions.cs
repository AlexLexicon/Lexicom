namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class AssemblyScanBuilderExtensions
{
    private static AssemblyScanOptions AssemblyScanOptions { get; } = new AssemblyScanOptions();

    public static AssemblyScanResult ForAssignableTo<TAssignableTo>(this AssemblyScanBuilder builder, AssemblyScanOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        options ??= AssemblyScanOptions;

        Type assignableTo = typeof(TAssignableTo);

        var foundTypes = new List<Type>();
        Type[] types = builder.AssemblyScanMarker.Assembly.GetExportedTypes();
        foreach (Type type in types)
        {
            if (type.IsAssignableTo(assignableTo) && (!type.IsAbstract || options.IncludeAbstract) && (!type.IsInterface || options.IncludeInterfaces))
            {
                foundTypes.Add(type);
            }
        }

        return new AssemblyScanResult(foundTypes);
    }
}
