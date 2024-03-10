using FluentValidation;
using Lexicom.DependencyInjection.Hosting;
using Lexicom.Validation.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Validation.For.Blazor.WebAssembly;
/*
 * the blazor host does not support ValidateOnStart() for options validator
 * so in order to implement that we have to find a round about way of attaching
 * some execution as soon as the blazor app 'Build()' is called
 * to do this I am providing a custom 'IServiceProviderFactory' to the host
 * which really just wraps the default one but we can use the 'CreateServiceProvider' method
 * to do the validation and other startup processes we might add in the future using the 'IDependencyInjectionHostPostBuildService' interface
 */
public class BlazorWebAssemblyValidateOnStartAfterServiceProviderBuildService : IAfterServiceProviderBuildService
{
    private static MethodInfo? _staticValidateOptionsMethodInfo;
    private static MethodInfo StaticValidateOptionsMethodInfo => _staticValidateOptionsMethodInfo ??= (typeof(BlazorWebAssemblyValidateOnStartAfterServiceProviderBuildService).GetMethod(nameof(ValidateOptions), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(ValidateOptions)}' was not found."));
    private static void ValidateOptions<TOptions>(IServiceProvider provider, string name) where TOptions : class
    {
        var validateOptions = provider.GetService<IValidateOptions<TOptions>>();
        if (validateOptions is not null)
        {
            var options = provider.GetService<IOptions<TOptions>>();

            if (options is not null)
            {
                validateOptions.Validate(name, options.Value);
            }
        }
    }

    public void OnAfterServiceProviderBuild(IServiceProvider provider)
    {
        IEnumerable<ValidateOptionsStartRegistration> validateOptionsStartRegistrations = provider.GetServices<ValidateOptionsStartRegistration>();

        foreach (ValidateOptionsStartRegistration validateOptionsStartRegistration in validateOptionsStartRegistrations)
        {
            MethodInfo validateOptionsMethodInfo = StaticValidateOptionsMethodInfo.MakeGenericMethod(validateOptionsStartRegistration.OptionsType);

            validateOptionsMethodInfo.Invoke(null,
            [
                provider,
                validateOptionsStartRegistration.OptionsName
            ]);
        }
    }
}
