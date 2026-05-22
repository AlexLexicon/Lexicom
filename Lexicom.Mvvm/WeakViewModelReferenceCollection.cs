using Lexicom.Mvvm.Exceptions;
using System.Collections;

namespace Lexicom.Mvvm;
public interface IWeakViewModelReferenceCollection
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ViewModelNotOfViewModelImplementationTypeException{TViewModelImplementation}"/>
    void Add(object viewModel);
}
public interface IWeakViewModelReferenceCollection<TViewModelImplementation> : IWeakViewModelReferenceCollection, IEnumerable<TViewModelImplementation> where TViewModelImplementation : class
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ViewModelNotOfViewModelImplementationTypeException{TViewModelImplementation}"/>
    void Add(TViewModelImplementation viewModel);
}
public class WeakViewModelReferenceCollection<TViewModelImplementation> : IWeakViewModelReferenceCollection<TViewModelImplementation> where TViewModelImplementation : class
{
    public WeakViewModelReferenceCollection()
    {
        WeakViewModelReferenceContainers = [];
        MutateLock = new Lock();

        PruneThreshold = 8;
    }

    //debug to figure out singleton issue
    public Guid Id { get; } = Guid.NewGuid();
    public string? Type { get; } = typeof(TViewModelImplementation).FullName;

    private List<WeakViewModelRefrenenceContainer<TViewModelImplementation>> WeakViewModelReferenceContainers { get; }
    private Lock MutateLock { get; }
    private int PruneThreshold { get; set; }
    private int CurrentAddedOrder { get; set; }

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
            var container = new WeakViewModelRefrenenceContainer<TViewModelImplementation>
            {
                AddedOrder = CurrentAddedOrder,
                WeakReference = new WeakReference<TViewModelImplementation>(viewModel),
            };

            CurrentAddedOrder++;
            WeakViewModelReferenceContainers.Add(container);

            if (WeakViewModelReferenceContainers.Count >= PruneThreshold)
            {
                GetViewModelsAndPruneDeadReferences();
            }
        }
    }

    //removes dead weak references in place and returns the still-live view
    //models in insertion order. callers must hold MutateLock.
    private IEnumerable<TViewModelImplementation> GetViewModelsAndPruneDeadReferences()
    {
        var sortedViewModels = new SortedList<int, TViewModelImplementation>();

        int writeIndex = 0;
        for (int readIndex = 0; readIndex < WeakViewModelReferenceContainers.Count; readIndex++)
        {
            WeakViewModelRefrenenceContainer<TViewModelImplementation> container = WeakViewModelReferenceContainers[readIndex];

            if (container.WeakReference.TryGetTarget(out TViewModelImplementation? viewModel))
            {
                sortedViewModels.Add(container.AddedOrder, viewModel);
                WeakViewModelReferenceContainers[writeIndex] = container;
                writeIndex++;
            }
        }

        WeakViewModelReferenceContainers.RemoveRange(writeIndex, WeakViewModelReferenceContainers.Count - writeIndex);

        PruneThreshold = Math.Max(PruneThreshold, sortedViewModels.Count * 2);

        return sortedViewModels.Values;
    }

    public IEnumerator<TViewModelImplementation> GetEnumerator()
    {
        lock (MutateLock)
        {
            return GetViewModelsAndPruneDeadReferences().GetEnumerator();
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
