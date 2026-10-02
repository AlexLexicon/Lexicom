using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Testing.DependencyInjection;

namespace Lexicom.Mvvm.For.Testing;

public class TestViewModelFactory : ViewModelFactory
{
    public TestViewModelFactory(
        IServiceProvider serviceProvider,
        IEnumerable<IMessenger> messengers,
        IEnumerable<IIntegrationTestAssistant> integrationTestAssistant)
        : base(integrationTestAssistant.FirstOrDefault() ?? serviceProvider, messengers)
    {
    }
}
