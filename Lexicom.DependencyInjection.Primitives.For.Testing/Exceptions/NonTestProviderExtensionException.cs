namespace Lexicom.DependencyInjection.Primitives.For.Testing.Exceptions;

public class NonTestProviderExtensionException<TInterface, TMockType>() : Exception($"The '{typeof(TInterface).Name}' instance used with this extension method is not of the type '{typeof(TMockType).Name}'. Only use this extension method when the '{typeof(TInterface).Name}' type has been mocked with the '{typeof(TMockType).Name}' type.")
{
}
