using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lexicom.SourceGenerator.MetaData;
public record struct MethodMetaData
{
    private readonly Compilation _compilation;
    private readonly MethodDeclarationSyntax _syntax;

    /// <exception cref="ArgumentNullException"/>
    public MethodMetaData(
        Compilation compilation,
        MethodDeclarationSyntax syntax)
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
        _signature = null;
        _accessibility = null;
        _isVoid = null;
        _returnTypeName = null;
        _returnTypeFullyQualifiedName = null;
        _isStatic = null;
        _isAbstract = null;
        _parameters = null;
        _symbolInfo = null;
        _methodSymbol = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = SymbolInfo.Name;

                if (string.IsNullOrWhiteSpace(_name))
                {
                    throw new FailedToParseMetaDataException<MethodMetaData>(nameof(Name), "it was null, empty or all whitespaces");
                }
            }

            return _name;
        }
    }

    private string? _signature;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public string Signature
    {
        get
        {
            if (_signature is null)
            {
                _signature = SymbolInfo.ToString();

                if (string.IsNullOrWhiteSpace(_signature))
                {
                    throw new FailedToParseMetaDataException<MethodMetaData>(nameof(Signature), "it was null, empty or all whitespaces");
                }
            }

            return _signature;
        }
    }

    private Accessibility? _accessibility;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public Accessibility Accessibility => _accessibility ??= MethodSymbol.DeclaredAccessibility;

    private bool? _isVoid;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public bool IsVoid => _isVoid ??= MethodSymbol.ReturnsVoid;

    private string? _returnTypeName;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public string ReturnTypeName => _returnTypeName ??= MethodSymbol.ReturnType.Name;

    private string? _returnTypeFullyQualifiedName;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public string ReturnTypeFullyQualifiedName
    {
        get
        {
            if (_returnTypeFullyQualifiedName is null)
            {
                _returnTypeFullyQualifiedName = MethodSymbol.ReturnType.ToString();

                if (string.IsNullOrWhiteSpace(_returnTypeFullyQualifiedName))
                {
                    throw new FailedToParseMetaDataException<MethodMetaData>(nameof(ReturnTypeFullyQualifiedName), "it was null, empty or all whitespaces");
                }
            }
            return _returnTypeFullyQualifiedName;
        }
    }

    private bool? _isStatic;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public bool IsStatic => _isStatic ??= MethodSymbol.IsStatic;

    private bool? _isAbstract;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public bool IsAbstract => _isAbstract ??= MethodSymbol.IsAbstract;

    private List<ParameterMetaData>? _parameters;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    public IReadOnlyList<ParameterMetaData> Parameters
    {
        get
        {
            if (_parameters is null)
            {
                _parameters = new List<ParameterMetaData>();

                foreach (IParameterSymbol parameter in MethodSymbol.Parameters)
                {
                    _parameters.Add(new ParameterMetaData(parameter));
                }
            }

            return _parameters;
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
                    throw new FailedToParseMetaDataException<MethodMetaData>(nameof(SymbolInfo), "it was null");
                }
            }
            return _symbolInfo;
        }
    }

    private IMethodSymbol? _methodSymbol;
    /// <exception cref="FailedToParseMetaDataException{MethodDescription}"/>
    private IMethodSymbol MethodSymbol
    {
        get
        {
            if (_methodSymbol is null)
            {
                if (SymbolInfo is not IMethodSymbol methodSymbol)
                {
                    throw new FailedToParseMetaDataException<MethodMetaData>(nameof(MethodSymbol), $"the '{nameof(SymbolInfo)}' was not a '{nameof(IMethodSymbol)}'");
                }

                _methodSymbol = methodSymbol;
            }

            return _methodSymbol;
        }
    }

    public override string ToString() => Name;
}
