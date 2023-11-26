using Lexicom.Mvvm.Support;

namespace Lexicom.Mvvm;
public interface IViewModelFactory
{
    TViewModel Create<TViewModel>() where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel>(TModel model) where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull;
    TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull;
}
/// <exception cref="ArgumentNullException"/>
public class ViewModelFactory(IServiceProvider serviceProvider) : ViewModelProvider(serviceProvider), IViewModelFactory
{
    public TViewModel Create<TViewModel>() where TViewModel : notnull
    {
        return CreateViewModel<TViewModel>();
    }

    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel>(TModel model) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateViewModel<TViewModel, TModel>(model);
    }

    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return CreateViewModel<TViewModel, TModel1, TModel2>(model1, model2);
    }

    /// <exception cref="ArgumentNullException"/>
    public TViewModel Create<TViewModel, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return CreateViewModel<TViewModel, TModel1, TModel2, TModel3>(model1, model2, model3);
    }
}
