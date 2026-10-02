using Lexicom.Testing.DependencyInjection.Exceptions;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Exceptions;

namespace Lexicom.Testing.DependencyInjection.Extensions;

public static class ObjectExtensions
{
    public static bool IsSubstitute(this object? instance)
    {
        if (instance is null)
        {
            return false;
        }

        try
        {
            SubstitutionContext.Current.GetCallRouterFor(instance);

            return true;
        }
        catch (NotASubstituteException)
        {
            return false;
        }
    }

    /// <summary>
    /// Set a return value for this call.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="returnThese">return these values in order</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ReturnsTheseEmptyException"/>
    public static ConfiguredCall ReturnsThese<T>(this T value, IEnumerable<T> returnThese)
    {
        ArgumentNullException.ThrowIfNull(returnThese);

        int count = returnThese.Count();
        if (count is > 1)
        {
            return value.Returns(returnThese.First(), returnThese.Skip(1).ToArray());
        }
        else if (count is 1)
        {
            return value.Returns(returnThese.First());
        }

        throw new ReturnsTheseEmptyException();
    }
}
