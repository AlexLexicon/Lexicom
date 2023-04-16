using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Lexicom.Mvvm.For.Wpf;
public interface IViewModelWindowCoupler<TViewModelService> where TViewModelService : notnull
{
    Type WindowType { get; }
    /// <exception cref="ArgumentNullException"/>
    void Couple(TViewModelService dataContext, Window window);
}
public class ViewModelWindowCoupler<TViewModelService> : IViewModelWindowCoupler<TViewModelService> where TViewModelService : notnull
{
    /// <exception cref="ArgumentNullException"/>
    public ViewModelWindowCoupler(Type windowType)
    {
        ArgumentNullException.ThrowIfNull(windowType);

        WindowType = windowType;
    }

    public Type WindowType { get; }

    /// <exception cref="ArgumentNullException"/>
    public void Couple(TViewModelService viewModel, Window window)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        ArgumentNullException.ThrowIfNull(window);

        window.DataContext = viewModel;

        if (viewModel is ICloseableViewModel windowCloseViewModel)
        {
            windowCloseViewModel.CloseCommand = new RelayCommand(window.Close);
        }

        if (viewModel is IHideableViewModel windowHideViewModel)
        {
            windowHideViewModel.HideCommand = new RelayCommand(window.Hide);
        }

        if (viewModel is IShowableViewModel windowShowViewModel)
        {
            windowShowViewModel.ShowCommand = new RelayCommand(window.Show);
        }

        if (viewModel is IShowDialogableViewModel windowShowDialogViewModel)
        {
            windowShowDialogViewModel.ShowDialogCommand = new RelayCommand(() => window.ShowDialog());
        }
    }
}
