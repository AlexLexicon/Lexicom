using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Options;
public class LexicomOptionsBuilder<TOptions>(IServiceCollection services, string? name) : OptionsBuilder<TOptions>(services, name) where TOptions : class
{
}
