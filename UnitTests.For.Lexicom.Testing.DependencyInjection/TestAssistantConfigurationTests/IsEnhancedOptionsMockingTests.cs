using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NSubstitute;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.TestAssistantConfigurationTests;

public class IsEnhancedOptionsMockingTests
{
    [Fact]
    public void MicrosoftExtensionsOptions_Substitutes_Are_Enhanced_While_IsEnhancedOptionsMocking_Is_True()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsEnhancedOptionsMocking = true,
        });

        //act
        var uot = uta.Make<ServiceWithOptionsDependencies>();

        //assert
        Assert.NotNull(uot._modelOptions);
        Assert.NotNull(uot._modelSnapshotOptions);
        Assert.NotNull(uot._modelMonitorOptions);

        Assert.True(uot._modelOptions.IsSubstitute());
        Assert.True(uot._modelSnapshotOptions.IsSubstitute());
        Assert.True(uot._modelMonitorOptions.IsSubstitute());

        Assert.True(uot._modelOptions.Value.IsSubstitute());
        Assert.True(uot._modelSnapshotOptions.Value.IsSubstitute());
        Assert.True(uot._modelMonitorOptions.CurrentValue.IsSubstitute());

        Assert.NotNull(uot._modelOptions.Value.StringValue);
        Assert.NotNull(uot._modelSnapshotOptions.Value.StringValue);
        Assert.NotNull(uot._modelSnapshotOptions.Get("test").StringValue);
        Assert.NotNull(uot._modelMonitorOptions.CurrentValue.StringValue);

        Assert.Equal(string.Empty, uot._modelOptions.Value.StringValue);
        Assert.Equal(string.Empty, uot._modelSnapshotOptions.Value.StringValue);
        Assert.Equal(string.Empty, uot._modelSnapshotOptions.Get("test").StringValue);
        Assert.Equal(string.Empty, uot._modelMonitorOptions.CurrentValue.StringValue);
    }

    [Fact]
    public void MicrosoftExtensionsOptions_Enhanced_Substitutes_Use_Mocked_Generic_Type_While_IsEnhancedOptionsMocking_Is_True()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsEnhancedOptionsMocking = true,
        });

        uta.Mock<OptionsModel>().So(om =>
        {
            om.StringValue = "test";
        });

        //act
        var uot = uta.Make<ServiceWithOptionsDependencies>();

        //assert
        Assert.NotNull(uot._modelOptions);
        Assert.NotNull(uot._modelSnapshotOptions);
        Assert.NotNull(uot._modelMonitorOptions);

        Assert.True(uot._modelOptions.IsSubstitute());
        Assert.True(uot._modelSnapshotOptions.IsSubstitute());
        Assert.True(uot._modelMonitorOptions.IsSubstitute());

        Assert.True(uot._modelOptions.Value.IsSubstitute());
        Assert.True(uot._modelSnapshotOptions.Value.IsSubstitute());
        Assert.True(uot._modelMonitorOptions.CurrentValue.IsSubstitute());

        Assert.Equal("test", uot._modelOptions.Value.StringValue);
        Assert.Equal("test", uot._modelSnapshotOptions.Value.StringValue);
        Assert.Equal("test", uot._modelSnapshotOptions.Get("test").StringValue);
        Assert.Equal("test", uot._modelMonitorOptions.CurrentValue.StringValue);
    }

    [Fact]
    public void MicrosoftExtensionsOptions_Substitutes_Are_Not_Enhanced_While_IsEnhancedOptionsMocking_Is_False()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsEnhancedOptionsMocking = false,
        });

        //act
        var uot = uta.Make<ServiceWithOptionsDependencies>();

        //assert
        Assert.NotNull(uot._modelOptions);
        Assert.NotNull(uot._modelSnapshotOptions);
        Assert.NotNull(uot._modelMonitorOptions);

        Assert.True(uot._modelOptions.IsSubstitute());
        Assert.True(uot._modelSnapshotOptions.IsSubstitute());
        Assert.True(uot._modelMonitorOptions.IsSubstitute());

        Assert.Null(uot._modelOptions.Value);
        Assert.Null(uot._modelSnapshotOptions.Value);
        Assert.Null(uot._modelMonitorOptions.CurrentValue);
    }

    [Fact]
    public void MicrosoftExtensionsOptions_Mocked_Substitutes_So_Is_Not_Overridden_While_IsEnhancedOptionsMocking_Is_True()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsEnhancedOptionsMocking = true,
        });

        var optionsModel = new OptionsModel
        {
            StringValue = "options",
        };
        var snapshotOptionsModel = new OptionsModel
        {
            StringValue = "snapshot",
        };
        var monitorOptionsModel = new OptionsModel
        {
            StringValue = "monitor",
        };

        uta.Mock<IOptions<OptionsModel>>().So(o =>
        {
            o.Value.Returns(optionsModel);
        });
        uta.Mock<IOptionsSnapshot<OptionsModel>>().So(o =>
        {
            o.Value.Returns(snapshotOptionsModel);
        });
        uta.Mock<IOptionsMonitor<OptionsModel>>().So(o =>
        {
            o.CurrentValue.Returns(monitorOptionsModel);
        });

        //act
        var uot = uta.Make<ServiceWithOptionsDependencies>();

        //assert
        Assert.NotNull(uot);

        Assert.Same(optionsModel, uot._modelOptions.Value);
        Assert.Same(snapshotOptionsModel, uot._modelSnapshotOptions.Value);
        Assert.Same(monitorOptionsModel, uot._modelMonitorOptions.CurrentValue);

        //the parts of the substitute that were not configured are still enhanced
        Assert.True(uot._modelSnapshotOptions.Get("test").IsSubstitute());
        Assert.Equal(string.Empty, uot._modelSnapshotOptions.Get("test").StringValue);
    }

    [Fact]
    public void MicrosoftExtensionsOptions_Pulled_Substitute_Configuration_Is_Not_Overridden_By_Make_While_IsEnhancedOptionsMocking_Is_True()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsEnhancedOptionsMocking = true,
        });

        var optionsModel = new OptionsModel
        {
            StringValue = "options",
        };

        IOptions<OptionsModel> options = uta.Mock<IOptions<OptionsModel>>(MockLifetime.Singleton).Pull();

        options.Value.Returns(optionsModel);

        //act
        var uot = uta.Make<ServiceWithOptionsDependencies>();

        //assert
        Assert.NotNull(uot);

        Assert.Same(options, uot._modelOptions);
        Assert.Same(optionsModel, uot._modelOptions.Value);
    }
}
