using Lexicom.Testing.DependencyInjection.Mocking;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection.EntityFramework.Mocking;

public static class MockHook
{
    public static MockHooksManager.HookDelegate HookDelegate { get; } = TryMock;

    private static bool TryMock(MockManager manager, Type type)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(type);

        Type genericType = type.GetGenericTypeDefinition();
        if (genericType == typeof(IDbContextFactory<>))
        {
            Type dbContextType = type.GetGenericArguments()[0];

            Type dbContextFactoryInterfaceType = genericType.MakeGenericType(dbContextType);
            Type dbContextFactoryConcreteType = typeof(SqliteInMemoryTestDbContextFactory<>).MakeGenericType(dbContextType);

            manager.Mock(dbContextFactoryInterfaceType, MockLifetime.Singleton).With(dbContextFactoryConcreteType);

            return true;
        }

        return false;
    }
}
