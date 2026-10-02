namespace Lexicom.Testing.DependencyInjection;

public class TestAssistantConfiguration
{
    /// <summary>
    /// While true all instances created with the Make<TImplementation>() call will be created with <see cref="NSubstitute"/> substitutions used for all non value type constructor parameters that do not already have a mock configured with the <see cref="UnitTestAssistant"/> Mock<TImplementation>() call.
    /// </summary>
    public bool IsAutomaticallyMocking { get; set; } = true;

    /// <summary>
    /// While true all instances created with the Make<TImplementation> call will have enhanced <see cref="NSubstitute"/> substitutions for any of the Microsoft.Extensions.Options types including <see cref="IOptions<TImplementation>"/>, <see cref="IOptionsSnapshot<TImplementation>"/> and <see cref="IOptionsMonitor<TImplementation>"/>. Specifically these substitutions will have their corresponding 'Value' or 'CurrentValue' properties set to an instance of an <see cref="NSubstitute"/> substitution rather than be null.
    /// </summary>
    public bool IsEnhancedOptionsMocking { get; set; } = true;

    /// <summary>
    /// This value will control what the lifetime of any mock that has not had its lifetime specifically configured with the <see cref="UnitTestAssistant"/> Mock<TImplementation>() call.
    /// </summary>
    public MockLifetime DefaultMockLifetime { get; set; } = MockLifetime.Singleton;

    /// <summary>
    /// While true all instances created with the Make<TImplementation> call will possibly be created with custom mocked dependencies from hooked packages. For example if you have the Lexicom.UnitTesting.DependencyInjection.EntityFramework package installed then any IDbContextFactory<TDbContext> constructor parameters will be automatically replaced with a custom in memory sqlite test database.
    /// </summary>
    public bool IsAutomaticallyUsingMockHooks { get; set; } = true;
}
