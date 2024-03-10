namespace Lexicom.DependencyInjection.Amenities;
public class AssignableToOptions
{
    public static readonly AssignableToOptions Default = new AssignableToOptions();

    public bool AllowAbstract { get; set; }
    public bool AllowInterfaces { get; set; }
}
