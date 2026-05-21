using Lexicom.Mvvm.Exceptions;
using System.Collections;

namespace Lexicom.Mvvm;
public interface IWeakViewModelReferenceCollection
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ViewModelNotOfViewModelImplementationTypeException{TViewModelImplementation}"></exception>
    void Add(object viewModel);
}
public interface IWeakViewModelReferenceCollection<TViewModelImplementation> : IWeakViewModelReferenceCollection, IEnumerable<TViewModelImplementation> where TViewModelImplementation : class
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ViewModelNotOfViewModelImplementationTypeException{TViewModelImplementation}"></exception>
    void Add(TViewModelImplementation viewModel);
}
public class WeakViewModelReferenceCollection<TViewModelImplementation> : IWeakViewModelReferenceCollection<TViewModelImplementation> where TViewModelImplementation : class
{
    public WeakViewModelReferenceCollection()
    {
        WeakViewModelReferences = [];
        MutateLock = new Lock();

        PruneThreshold = 8;
    }

    private List<WeakReference<TViewModelImplementation>> WeakViewModelReferences { get; }
    private Lock MutateLock { get; }
    private int PruneThreshold { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public void Add(object viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (viewModel is TViewModelImplementation viewModelImplementation)
        {
            Add(viewModelImplementation);
        }
        else
        {
            throw new ViewModelNotOfViewModelImplementationTypeException<TViewModelImplementation>(viewModel);
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public void Add(TViewModelImplementation viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        lock (MutateLock)
        {
            WeakViewModelReferences.Add(new WeakReference<TViewModelImplementation>(viewModel));

            if (WeakViewModelReferences.Count >= PruneThreshold)
            {
                PruneDeadReferences();
            }
        }
    }

    public IReadOnlyList<TViewModelImplementation> GetRemainingViewModels()
    {
        lock (MutateLock)
        {
            return PruneDeadReferences();
        }
    }

    //removes dead weak references in place and returns the still-live view
    //models in insertion order. callers must hold MutateLock.
    private List<TViewModelImplementation> PruneDeadReferences()
    {
        var viewModels = new List<TViewModelImplementation>();

        int writeIndex = 0;
        for (int readIndex = 0; readIndex < WeakViewModelReferences.Count; readIndex++)
        {
            WeakReference<TViewModelImplementation> weakViewModelRefrence = WeakViewModelReferences[readIndex];

            if (weakViewModelRefrence.TryGetTarget(out TViewModelImplementation? viewModel))
            {
                //if the view model is disposed we should not include it
                if (viewModel is not DisposableObservableObject disposableViewModel || !disposableViewModel.IsDisposed)
                {
                    viewModels.Add(viewModel);
                    WeakViewModelReferences[writeIndex] = weakViewModelRefrence;
                    writeIndex++;
                }
            }
        }

        WeakViewModelReferences.RemoveRange(writeIndex, WeakViewModelReferences.Count - writeIndex);

        PruneThreshold = Math.Max(PruneThreshold, viewModels.Count * 2);

        return viewModels;
    }

    public IEnumerator<TViewModelImplementation> GetEnumerator() => GetRemainingViewModels().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
