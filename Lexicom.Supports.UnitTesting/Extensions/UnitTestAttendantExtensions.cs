using Lexicom.Supports.UnitTesting.Options;
using Lexicom.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Supports.UnitTesting.Extensions;
public static class UnitTestAttendantExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static UnitTestAttendant Mock<T>(this UnitTestAttendant builder) where T : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Mock<T>(m => { });

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static UnitTestAttendant MockOptions<TOptions>(this UnitTestAttendant unitTestAttendant, TOptions value) where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(unitTestAttendant);

        unitTestAttendant.AddSingleton(sp =>
        {
            return Microsoft.Extensions.Options.Options.Create(value);
        });

        unitTestAttendant.AddSingleton<IOptionsMonitor<TOptions>>(sp =>
        {
            return new TestOptionsMonitor<TOptions>(value);
        });

        unitTestAttendant.AddSingleton<IOptionsSnapshot<TOptions>>(sp =>
        {
            return new TestOptionsSnapshot<TOptions>(value);
        });

        return unitTestAttendant;
    }
}
