using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Testing.Extensions;
using Lexicom.Mvvm.UnitTests.Constructs.Services;
using Lexicom.Mvvm.UnitTests.Constructs.ViewModels;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.UnitTests.Tests;

public class ViewModelProviderTests
{
    [Fact]
    public void ViewModelProvider_Returns_Non_Disposed_ViewModels()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<HeaderViewModel>();
            });
        });

        ita.Mock<IAccountService>();

        var viewModelFactory = ita.GetRequiredService<IViewModelFactory>();
        var provider = ita.GetRequiredService<IViewModelProvider<HeaderViewModel>>();

        //act
        var initalViewModels = provider.GetViewModels();

        var expectedVm1 = viewModelFactory.Create<HeaderViewModel>();
        var expectedVm2 = viewModelFactory.Create<HeaderViewModel>();
        var expectedVm3 = viewModelFactory.Create<HeaderViewModel>();

        var laterViewModels = provider.GetViewModels();

        var laterVm1 = laterViewModels[0];
        var laterVm2 = laterViewModels[1];
        var laterVm3 = laterViewModels[2];

        laterVm1.Dispose();

        var finalViewModels = provider.GetViewModels();

        var finalVm2 = finalViewModels[0];
        var finalVm3 = finalViewModels[1];

        //assert
        Assert.NotNull(initalViewModels);
        Assert.Empty(initalViewModels);

        Assert.NotNull(laterViewModels);
        Assert.NotEmpty(laterViewModels);
        Assert.Equal(3, laterViewModels.Count);
        Assert.Equal(expectedVm1, laterVm1);
        Assert.Equal(expectedVm2, laterVm2);
        Assert.Equal(expectedVm3, laterVm3);

        Assert.NotNull(finalViewModels);
        Assert.NotEmpty(finalViewModels);
        Assert.Equal(2, finalViewModels.Count);
        Assert.Equal(expectedVm2, finalVm2);
        Assert.Equal(expectedVm3, finalVm3);
    }
}
