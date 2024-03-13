namespace Lexicom.DependencyInjection.Hosting;
public enum ServiceProviderBuildPriority
{
    First = int.MinValue,
    Middle = 0,
    Last = int.MaxValue,
}
