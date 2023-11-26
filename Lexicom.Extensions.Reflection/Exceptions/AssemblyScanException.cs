namespace Lexicom.Extensions.Reflection.Exceptions;
public class AssemblyScanException<TAssemblyScanMarker, TInterface>(Exception? innerException) : Exception($"Failed while assembly scanning for '{typeof(TInterface).FullName}' from the assembly scan marker '{typeof(TAssemblyScanMarker).FullName}'.", innerException)
{
}
