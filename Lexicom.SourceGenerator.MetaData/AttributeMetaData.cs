using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lexicom.SourceGenerator.MetaData;
public record struct AttributeMetaData
{
    private readonly Compilation _compilation;
    private readonly AttributeSyntax _syntax;

    /// <exception cref="ArgumentNullException"/>
    public AttributeMetaData(
        Compilation compilation,
        AttributeSyntax syntax)
    {
        if (compilation is null)
        {
            throw new ArgumentNullException(nameof(compilation));
        }
        if (syntax is null)
        {
            throw new ArgumentNullException(nameof(syntax));
        }

        _compilation = compilation;
        _syntax = syntax;

        _name = null;
        _fullyQualifiedName = null;
        _arguments = null;
        _symbolInfo = null;
        _attributeNamedTypeSymbol = null;
    }

    private string? _name;
    public string Name => _name ??= AttributeNamedTypeSymbol.Name;

    private string? _fullyQualifiedName;
    /// <exception cref="FailedToParseMetaDataException{AttributeDescription}"/>
    public string FullyQualifiedName
    {
        get
        {
            if (_fullyQualifiedName is null)
            {
                _fullyQualifiedName = AttributeNamedTypeSymbol.ToString();

                if (string.IsNullOrWhiteSpace(_fullyQualifiedName))
                {
                    throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(FullyQualifiedName), "it was null, empty or all whitespaces");
                }
            }

            return _fullyQualifiedName;
        }
    }

    private List<AttributeArgumentMetaData>? _arguments;
    /// <exception cref="FailedToParseMetaDataException{AttributeDescription}"/>
    public IReadOnlyList<AttributeArgumentMetaData> Arguments
    {
        get
        {
            if (_arguments is null)
            {
                _arguments = new List<AttributeArgumentMetaData>();

                if (_syntax.ArgumentList is not null && SymbolInfo is IMethodSymbol methodSymbol)
                {
                    int parametersCount = methodSymbol.Parameters.Length;

                    if (_syntax.ArgumentList.Arguments.Count != parametersCount)
                    {
                        throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(Arguments), "the number of parameters and number of provided arguments did not match");
                    }

                    for (int index = 0; index < parametersCount; index++)
                    {
                        AttributeArgumentSyntax argument = _syntax.ArgumentList.Arguments[index];
                        IParameterSymbol parameter = methodSymbol.Parameters[index];

                        _arguments.Add(new AttributeArgumentMetaData(_compilation, argument, parameter));
                    }
                }
            }

            return _arguments;
        }
    }

    private ISymbol? _symbolInfo;
    /// <exception cref="FailedToParseMetaDataException{AttributeDescription}"/>
    private ISymbol SymbolInfo
    {
        get
        {
            if (_symbolInfo is null)
            {
                _symbolInfo = _compilation.GetSemanticModel(_syntax.SyntaxTree).GetSymbolInfo(_syntax).Symbol;

                if (_symbolInfo is null)
                {
                    throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(SymbolInfo), "it was null");
                }
            }
            return _symbolInfo;
        }
    }

    private INamedTypeSymbol? _attributeNamedTypeSymbol;
    /// <exception cref="FailedToParseMetaDataException{AttributeDescription}"/>
    private INamedTypeSymbol AttributeNamedTypeSymbol
    {
        get
        {
            if (_attributeNamedTypeSymbol is null)
            {
                if (SymbolInfo.ContainingType is null)
                {
                    throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(AttributeNamedTypeSymbol), $"the '{nameof(SymbolInfo)}.{nameof(SymbolInfo.ContainingType)}' was null");
                }

                if (SymbolInfo.ContainingType is not INamedTypeSymbol attributeNamedTypeSymbol)
                {
                    throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(AttributeNamedTypeSymbol), $"the '{nameof(SymbolInfo)}.{nameof(SymbolInfo.ContainingType)}' was not a '{nameof(INamedTypeSymbol)}'");
                }

                _attributeNamedTypeSymbol = attributeNamedTypeSymbol;

                if (_attributeNamedTypeSymbol is null)
                {
                    throw new FailedToParseMetaDataException<AttributeMetaData>(nameof(AttributeNamedTypeSymbol), $"the '{nameof(attributeNamedTypeSymbol)}' was null");
                }
            }
            return _attributeNamedTypeSymbol;
        }
    }

    public override string ToString() => FullyQualifiedName;
}
