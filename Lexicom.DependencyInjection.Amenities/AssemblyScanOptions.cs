namespace Lexicom.DependencyInjection.Amenities;
public class AssemblyScanOptions
{
    public static readonly AssemblyScanOptions Default = new AssemblyScanOptions();

    public bool AllowNonExportedTypes { get; set; }
    public AssignableToOptions AssignableToOptions { get; set; } = AssignableToOptions.Default;
}
