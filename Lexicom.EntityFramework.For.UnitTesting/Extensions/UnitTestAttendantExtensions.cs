using Lexicom.UnitTesting;

namespace Lexicom.EntityFramework.UnitTesting.Extensions;
public static class UnitTestAttendantExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static UnitTestAttendant AddTestEntityFramework(this UnitTestAttendant builder, Action<IEntityFrameworkUnitTestingServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new EntityFrameworkUnitTestingServiceBuilder(builder, builder.Configuration));

        return builder;
    }
}
