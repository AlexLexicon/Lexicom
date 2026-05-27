using Lexicom.Mvvm.Exceptions;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Testing.Extensions;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Extensions;
using IntegrationTests.For.Lexicom.Mvvm.For.Testing.Constructs;

namespace IntegrationTests.For.Lexicom.Mvvm.For.Testing;

public class MakeTests
{
    [Fact]
    public void Successfully_Make_ViewModel_During_Integration_Test()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<SubViewModel>();
            });
        });

        //act
        var vm = ita.Make<SubViewModel>();

        //assert
        Assert.NotNull(vm);
        Assert.False(vm.IsSubstitute());
    }

    [Fact]
    public void Fail_To_Make_ViewModel_When_It_Injects_ViewModel_Thats_Not_Registered_During_Integration_Test()
    {
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<MainViewModel>();
            });
        });

        //assert
        Assert.Throws<ViewModelNotRegisteredException>(() =>
        {
            ita.Make<MainViewModel>();
        }, e =>
        {
            if (!e.Message.Contains(nameof(SubViewModel), StringComparison.OrdinalIgnoreCase) || e.Message.Contains(nameof(MainViewModel), StringComparison.OrdinalIgnoreCase))
            {
                return "The expected exception did not have the expected message.";
            }

            return null;
        });
    }

    [Fact]
    public void Successfully_Make_ViewModel_When_It_Injects_ViewModel_Thats_Not_Registered_During_Unit_Test()
    {
        //arrange
        var uta = new UnitTestAssistant();

        //act
        var vm = uta.Make<MainViewModel>();

        //assert
        Assert.NotNull(vm.SubViewModel);
        Assert.True(vm.SubViewModel.IsSubstitute());
    }
}
