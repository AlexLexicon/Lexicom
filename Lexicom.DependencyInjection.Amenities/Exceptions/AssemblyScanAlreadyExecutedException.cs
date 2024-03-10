namespace Lexicom.DependencyInjection.Amenities.Exceptions;
public class AssemblyScanAlreadyExecutedException(Type? assemblyScanMarkerType, Type? assignableToType) : Exception($"The assembly scan on the marker '{assemblyScanMarkerType?.FullName ?? "null"}' for the assignable type '{assignableToType?.FullName ?? "null"}' has already been executed and therfore cannot be executed again.")
{
}
