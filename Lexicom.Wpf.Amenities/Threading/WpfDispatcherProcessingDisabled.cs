using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Threading;
public class WpfDispatcherProcessingDisabled(DispatcherProcessingDisabled dispatcherProcessingDisabled) : IDispatcherProcessingDisabled
{
    public static bool operator ==(WpfDispatcherProcessingDisabled left, WpfDispatcherProcessingDisabled right) => left?._dispatcherProcessingDisabled == right?._dispatcherProcessingDisabled;
    public static bool operator !=(WpfDispatcherProcessingDisabled left, WpfDispatcherProcessingDisabled right) => left?._dispatcherProcessingDisabled != right?._dispatcherProcessingDisabled;

    private readonly DispatcherProcessingDisabled _dispatcherProcessingDisabled = dispatcherProcessingDisabled;

    public override bool Equals(object? obj) => _dispatcherProcessingDisabled.Equals(obj);
    public override int GetHashCode() => _dispatcherProcessingDisabled.GetHashCode();

    public void Dispose() => _dispatcherProcessingDisabled.Dispose();
}
