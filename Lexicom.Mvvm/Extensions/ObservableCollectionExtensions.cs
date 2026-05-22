using System.Collections.ObjectModel;

namespace Lexicom.Mvvm.Extensions;

public static class ObservableCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void DisposeChildren<T>(this ObservableCollection<T> collection) where T : IDisposable
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (T item in collection)
        {
            item.Dispose();
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static void DisposeAndClearChildren<T>(this ObservableCollection<T> collection) where T : IDisposable
    {
        ArgumentNullException.ThrowIfNull(collection);

        collection.DisposeChildren();
        collection.Clear();
    }
}
