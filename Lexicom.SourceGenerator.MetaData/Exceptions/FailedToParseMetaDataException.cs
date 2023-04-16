namespace Lexicom.SourceGenerator.MetaData.Exceptions;
public class FailedToParseMetaDataException<TMetaData> : MetaDataSourceGeneratorException
{
    public FailedToParseMetaDataException(string? PropertyName, string? reason) : base($"Failed to parse the '{typeof(TMetaData).Name}{(string.IsNullOrWhiteSpace(PropertyName) ? "" : $".{PropertyName}")}' property{(string.IsNullOrWhiteSpace(reason) ? "." : $" because {reason}.")}")
    {
    }
}
