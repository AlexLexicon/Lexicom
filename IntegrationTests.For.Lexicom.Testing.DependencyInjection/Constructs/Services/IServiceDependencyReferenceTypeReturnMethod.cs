using IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public interface IServiceDependencyReferenceTypeReturnMethod
{
    ReferenceTypeModel GetReferenceTypeModelMethod();
    Task<ReferenceTypeModel> GetReferenceTypeModelMethodAsync();
}
