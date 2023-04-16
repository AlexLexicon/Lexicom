using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Lexicom.SourceGenerator.MetaData.Exceptions;

namespace Lexicom.SourceGenerator.MetaData;
public record struct PropertyMetaData
{
    private readonly Compilation _compilation;
    private readonly PropertyDeclarationSyntax _syntax;

    /// <exception cref="ArgumentNullException"/>
    public PropertyMetaData(
        Compilation compilation,
        PropertyDeclarationSyntax syntax)
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
        _symbolInfo = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{PropertyDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = SymbolInfo.Name;

                if (string.IsNullOrWhiteSpace(_name))
                {
                    throw new FailedToParseMetaDataException<PropertyMetaData>(nameof(Name), "it was null, empty or all whitespaces");
                }
            }

            return _name;
        }
    }

    private ISymbol? _symbolInfo;
    private ISymbol SymbolInfo
    {
        get
        {
            if (_symbolInfo is null)
            {
                _symbolInfo = _compilation.GetSemanticModel(_syntax.SyntaxTree).GetDeclaredSymbol(_syntax);

                if (_symbolInfo is null)
                {
                    throw new FailedToParseMetaDataException<PropertyMetaData>(nameof(SymbolInfo), "it was null");
                }
            }
            return _symbolInfo;
        }
    }

    public override string ToString() => Name;
}
