using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public interface IServiceDependencyReferenceTypeReturnMethod
{
    ReferenceTypeModel GetReferenceTypeModelMethod();
    Task<ReferenceTypeModel> GetReferenceTypeModelMethodAsync();
}
