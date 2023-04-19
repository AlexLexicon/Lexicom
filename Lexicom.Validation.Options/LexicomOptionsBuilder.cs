using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Options;
public class LexicomOptionsBuilder<TOptions> : OptionsBuilder<TOptions> where TOptions : class
{
    public LexicomOptionsBuilder(IServiceCollection services, string? name) : base(services, name)
    {
    }
}
