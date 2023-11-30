using Lexicom.DependencyInjection.Hosting;
using Lexicom.Validation.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Validation.For.Blazor.WebAssembly;
public class BlazorWebAssemblyValidateOnStartPostBuildService : IDependencyInjectionHostPostBuildService
{
    private static MethodInfo? _staticValidateOptionsMethodInfo;
    private static MethodInfo StaticValidateOptionsMethodInfo => _staticValidateOptionsMethodInfo ??= (typeof(BlazorWebAssemblyValidateOnStartPostBuildService).GetMethod(nameof(ValidateOptions), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(ValidateOptions)}' was not found."));
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

    public void Run(IServiceProvider provider)
    {
        IEnumerable<ValidateOptionsStartRegistration> validateOptionsStartRegistrations = provider.GetServices<ValidateOptionsStartRegistration>();

        foreach (ValidateOptionsStartRegistration validateOptionsStartRegistration in validateOptionsStartRegistrations)
        {
            MethodInfo validateOptionsMethodInfo = StaticValidateOptionsMethodInfo.MakeGenericMethod(validateOptionsStartRegistration.OptionsType);

            validateOptionsMethodInfo.Invoke(null, new object[]
            {
                provider,
                validateOptionsStartRegistration.OptionsName
            });
        }
    }
}
