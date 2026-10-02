using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithReferenceTypeModelDependency
{
    public ServiceWithReferenceTypeModelDependency(ReferenceTypeModel referenceTypeModel)
    {
        ReferenceTypeModel = referenceTypeModel;
    }

    public ReferenceTypeModel ReferenceTypeModel { get; }
}
