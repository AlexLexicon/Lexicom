namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class MakeUnusedManualParametersException(Type? type, IEnumerable<string?>? unusedManualParameterTypeNames) : Exception($"The {nameof(TestAssistant)} failed to make an instance from the type '{type?.Name ?? "null"}' because one or more of the manually provided parameters were not resolved to the constructor parameters.{(unusedManualParameterTypeNames is null || !unusedManualParameterTypeNames.Any() ? string.Empty : $" '{string.Join(",", unusedManualParameterTypeNames.Where(mptn => mptn is not null))}'.")}")
{
}
