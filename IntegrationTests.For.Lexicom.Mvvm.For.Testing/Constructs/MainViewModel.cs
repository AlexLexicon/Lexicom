using CommunityToolkit.Mvvm.ComponentModel;

namespace IntegrationTests.For.Lexicom.Mvvm.For.Testing.Constructs;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(SubViewModel subViewModel)
    {
        SubViewModel = subViewModel;
    }

    [ObservableProperty]
    public SubViewModel _subViewModel;
}
