using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.For.Blazor.WebAssembly;
public class BlazorWebAssemblyValidationServiceBuilder : ValidationServiceBuilder, IBlazorWebAssemblyValidationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public BlazorWebAssemblyValidationServiceBuilder(
        IServiceCollection services, 
        LanguageManager languageManager) : base(services, languageManager)
    {
    }
}
