using Lexicom.UnitTesting;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class UnitTestAttendantExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static UnitTestAttendant AddTestTimeProvider(this UnitTestAttendant builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddSingleton<ITimeProvider, TestTimeProvider>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static UnitTestAttendant AddTestGuidProvider(this UnitTestAttendant builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddSingleton<IGuidProvider, TestGuidProvider>();

        return builder;
    }
}
