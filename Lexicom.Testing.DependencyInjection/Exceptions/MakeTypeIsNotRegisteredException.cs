namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class MakeTypeIsNotRegisteredException(Type? type) : Exception($"Make cannot resolve the type '{type?.Name ?? "null"}', this is likely because that type is an interface and you haven't registered the implementation type in the '{nameof(IntegrationTestAssistant)}' service collection.")
{
}
