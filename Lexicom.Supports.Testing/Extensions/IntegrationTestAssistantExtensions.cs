using Lexicom.Testing.DependencyInjection;

namespace Lexicom.Supports.Testing.Extensions;

public static class IntegrationTestAssistantExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void TestLexicom(this IntegrationTestAssistant integrationTestAssistant, Action<ITestingServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(integrationTestAssistant);

        configure?.Invoke(new TestingServiceBuilder(integrationTestAssistant));
    }
}
