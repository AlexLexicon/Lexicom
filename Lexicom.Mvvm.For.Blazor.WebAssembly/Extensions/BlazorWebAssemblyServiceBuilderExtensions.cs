using Lexicom.Mvvm.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;

public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddMvvm(this IBlazorWebAssemblyServiceBuilder builder, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomMvvm(configure);
        builder.Services.AddSingleton<IMessengerScheduler, BlazorMessengerScheduler>();

        return builder;
    }
}
