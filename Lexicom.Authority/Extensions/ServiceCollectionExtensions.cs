using Lexicom.Authority.Options;
using Lexicom.Authority.Validators;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authority.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAuthority(this IServiceCollection services, Action<IAuthorityServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOptions<AuthorityOptions>()
            .BindConfiguration()
            .Validate<AuthorityOptions, AuthorityOptionsValidator>()
            .ValidateOnStart();

        configure?.Invoke(new AuthorityServiceBuilder(services));

        return services;
    }
}
