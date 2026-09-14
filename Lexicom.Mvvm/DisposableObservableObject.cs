using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lexicom.Mvvm;

public class DisposableObservableObject : ObservableObject, IDisposable
{
    public bool IsDisposed { get; private set; }

    public virtual void Dispose()
    {
        IsDisposed = true;
    }

    protected bool SetPropertyAndDispose<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) where T : class, IDisposable
    {
        if (ReferenceEquals(field, value))
        {
            return false;
        }

        if (IsDisposed)
        {
            value?.Dispose();

            return false;
        }

        T oldValue = field;

        SetProperty(ref field, value, propertyName);

        oldValue?.Dispose();

        return true;
    }
}
