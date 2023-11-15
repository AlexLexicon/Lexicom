using Lexicom.Extensions.Reflection.Exceptions;
using System.Reflection;

namespace Lexicom.Extensions.Reflection;
public static class AssemblyScanner
{
    /// <exception cref="AssemblyScanException{TAssemblyScanMarker, TInterface}"/>
    public static List<Type> GetConcreteTypesImplementingInterface<TAssemblyScanMarker, TInterface>()
    {
        List<Type>? types = null;

        Exception? assemblyScanException = null;
        try
        {
            types = Assembly
                .GetAssembly(typeof(TAssemblyScanMarker))?
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(TInterface).IsAssignableFrom(t))
                .ToList();
        }
        catch (Exception e)
        {
            assemblyScanException = e;
        }

        if (assemblyScanException is not null || types is null)
        {
            throw new AssemblyScanException<TAssemblyScanMarker, TInterface>(assemblyScanException);
        }

        return types;
    }
}
