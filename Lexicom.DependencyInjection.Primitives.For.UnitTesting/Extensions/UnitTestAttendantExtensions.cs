using Lexicom.UnitTesting;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class UnitTestAttendantExtensions
{
    public static UnitTestAttendant AddPrimitives(this UnitTestAttendant attendant, Action<ITestDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(attendant);

        configure?.Invoke(new TestDependencyInjectionPrimitivesServiceBuilder(attendant));

        return attendant;
    }
}
