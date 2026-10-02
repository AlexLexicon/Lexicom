namespace Lexicom.Testing.DependencyInjection;

/// <summary>
/// This lifetime does not necessarily refer to the instances of the mock, ie if you mock a dependency with an instance directly, and set the lifetime to transient, it will just always use that provided instance, the difference is that it will always call the MockInstantiater every time where as if the lifetime is singleton the MockInstantiater is only ever called once the first time the mock is pulled.
/// </summary>
public enum MockLifetime
{
    /// <summary>
    /// The MockInstantiater will only be called once the first time this mock is pulled and whatever instance is returned will always be used for this mock after.
    /// </summary>
    Singleton,
    /// <summary>
    /// Each MockInstantiater will be called every time this mock is pulled.
    /// </summary>
    Transient,
}
