using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;
using Microsoft.Extensions.Options;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithOptionsDependencies
{
    public readonly IOptions<OptionsModel> _modelOptions;
    public readonly IOptionsSnapshot<OptionsModel> _modelSnapshotOptions;
    public readonly IOptionsMonitor<OptionsModel> _modelMonitorOptions;

    public ServiceWithOptionsDependencies(
        IOptions<OptionsModel> modelOptions,
        IOptionsSnapshot<OptionsModel> modelSnapshotOptions,
        IOptionsMonitor<OptionsModel> modelMonitorOptions)
    {
        _modelOptions = modelOptions;
        _modelSnapshotOptions = modelSnapshotOptions;
        _modelMonitorOptions = modelMonitorOptions;
    }
}
