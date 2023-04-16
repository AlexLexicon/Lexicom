using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData;
public record struct BaseClassMetaData
{
    private readonly INamedTypeSymbol _derivedNamedTypeSymbol;

    /// <exception cref="ArgumentNullException"/>
    public BaseClassMetaData(INamedTypeSymbol derivedNamedTypeSymbol)
    {
        if (derivedNamedTypeSymbol is null)
        {
            throw new ArgumentNullException(nameof(derivedNamedTypeSymbol));
        }

        _derivedNamedTypeSymbol = derivedNamedTypeSymbol;

        _name = null;
        _fullyQualifiedName = null;
        _namedTypeSymbol = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{BaseClassDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = NamedTypeSymbol.Name;

                if (string.IsNullOrWhiteSpace(_name))
                {
                    throw new FailedToParseMetaDataException<BaseClassMetaData>(nameof(Name), "it was null, empty or all whitespaces");
                }
            }

            return _name;
        }
    }

    private string? _fullyQualifiedName;
    /// <exception cref="FailedToParseMetaDataException{BaseClassDescription}"/>
    public string FullyQualifiedName
    {
        get
        {
            if (_fullyQualifiedName is null)
            {
                _fullyQualifiedName = NamedTypeSymbol.ToString();

                if (string.IsNullOrWhiteSpace(_fullyQualifiedName))
                {
                    throw new FailedToParseMetaDataException<BaseClassMetaData>(nameof(FullyQualifiedName), "it was null, empty or all whitespaces");
                }
            }

            return _fullyQualifiedName;
        }
    }

    private INamedTypeSymbol? _namedTypeSymbol;
    /// <exception cref="FailedToParseMetaDataException{BaseClassDescription}"/>
    private INamedTypeSymbol NamedTypeSymbol
    {
        get
        {
            if (_namedTypeSymbol is null)
            {
                _namedTypeSymbol = _derivedNamedTypeSymbol.BaseType;

                if (_namedTypeSymbol is null)
                {
                    throw new FailedToParseMetaDataException<BaseClassMetaData>(nameof(NamedTypeSymbol), "it was null");
                }
            }

            return _namedTypeSymbol;
        }
    }

    public override string ToString() => FullyQualifiedName;
}