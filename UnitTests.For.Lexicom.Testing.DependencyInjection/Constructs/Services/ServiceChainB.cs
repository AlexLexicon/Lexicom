namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceChainB
{
    public readonly ServiceChainC _serviceChainC;

    public ServiceChainB(ServiceChainC serviceChainC)
    {
        _serviceChainC = serviceChainC;
    }

    public string GetBText()
    {
        return $"b{_serviceChainC.GetCText()}";
    }
}
