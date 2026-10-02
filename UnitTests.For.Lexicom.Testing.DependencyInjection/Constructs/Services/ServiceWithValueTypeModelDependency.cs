using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithValueTypeModelDependency
{
    public ServiceWithValueTypeModelDependency(ValueTypeModel valueTypeModel)
    {
        ValueTypeModel = valueTypeModel;
    }

    public ValueTypeModel ValueTypeModel { get; }
}
