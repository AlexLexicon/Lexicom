using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData;
public record struct InterfaceMetaData
{
    private readonly INamedTypeSymbol _interfaceNamedTypeSymbol;

    /// <exception cref="ArgumentNullException"/>
    public InterfaceMetaData(INamedTypeSymbol interfaceNamedTypeSymbol)
    {
        if (interfaceNamedTypeSymbol is null)
        {
            throw new ArgumentNullException(nameof(interfaceNamedTypeSymbol));
        }

        _interfaceNamedTypeSymbol = interfaceNamedTypeSymbol;

        _name = null;
        _fullyQualifiedName = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{InterfaceDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = _interfaceNamedTypeSymbol.Name;

                if (string.IsNullOrWhiteSpace(_name))
                {
                    throw new FailedToParseMetaDataException<InterfaceMetaData>(nameof(Name), "it was null, empty or all whitespaces");
                }
            }

            return _name;
        }
    }

    private string? _fullyQualifiedName;
    /// <exception cref="FailedToParseMetaDataException{InterfaceDescription}"/>
    public string FullyQualifiedName
    {
        get
        {
            if (_fullyQualifiedName is null)
            {
                _fullyQualifiedName = _interfaceNamedTypeSymbol.ToString();

                if (string.IsNullOrWhiteSpace(_fullyQualifiedName))
                {
                    throw new FailedToParseMetaDataException<InterfaceMetaData>(nameof(FullyQualifiedName), "it was null, empty or all whitespaces");
                }
            }

            return _fullyQualifiedName;
        }
    }

    public override string ToString() => FullyQualifiedName;
}
