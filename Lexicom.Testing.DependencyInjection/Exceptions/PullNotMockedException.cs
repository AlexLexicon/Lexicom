namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class PullNotMockedException(Type? type) : Exception($"Cannot pull an instance of the type '{type?.Name ?? "null"}' because it is not mocked. If you are not using '{nameof(TestAssistantConfiguration)}.{nameof(TestAssistantConfiguration.IsAutomaticallyMocking)}' then you must manually call '{nameof(UnitTestAssistant)}.{nameof(UnitTestAssistant.Mock)}()' on all constructor parameters for any type you are calling '{nameof(UnitTestAssistant)}.{nameof(UnitTestAssistant.Make)}()' on.")
{
}
