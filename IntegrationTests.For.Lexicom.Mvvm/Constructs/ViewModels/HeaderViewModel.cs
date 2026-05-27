using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Models;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class HeaderViewModel : DisposableObservableObject
{
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IAccountService _accountService;

    public HeaderViewModel(
        IViewModelFactory viewModelFactory,
        IAccountService accountService)
    {
        _viewModelFactory = viewModelFactory;
        _accountService = accountService;
    }

    [ObservableProperty]
    public partial ProfileViewModel? ProfileViewModel { get; set; }

    public override void Dispose()
    {
        base.Dispose();

        ProfileViewModel?.Dispose();
    }

    public async Task LoadAsync()
    {
        Account account = await _accountService.GetLoggedInAccountAsync();

        ProfileViewModel = _viewModelFactory.Create<ProfileViewModel, Account>(account);

        await ProfileViewModel.LoadAsync();
    }
}
