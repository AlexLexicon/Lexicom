using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData;
public record struct NamespaceMetaData
{
    private readonly INamedTypeSymbol _typeNamedTypeSymbol;

    /// <exception cref="ArgumentNullException"/>
    public NamespaceMetaData(INamedTypeSymbol typeNamedTypeSymbol)
    {
        if (typeNamedTypeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeNamedTypeSymbol));
        }

        _typeNamedTypeSymbol = typeNamedTypeSymbol;

        _name = null;
        _assembly = null;
        _path = null;
        _containgingAssembly = null;
        _containingNamespace = null;
        _assemblySymbol = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{NamespaceDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = Path;

                int lastIndexOfCharacter = Path.LastIndexOf('.');

                if (lastIndexOfCharacter >= 0)
                {
                    lastIndexOfCharacter++;

                    if (lastIndexOfCharacter < Path.Length)
                    {
                        _name = Path.Substring(lastIndexOfCharacter);
                    }
                }
            }

            return _name;
        }
    }

    /// <exception cref="FailedToParseMetaDataException{NamespaceDescription}"/>
    public bool IsRoot => Path is not null && Assembly is not null && Path == Assembly;


    private string? _assembly;
    /// <exception cref="FailedToParseMetaDataException{NamespaceDescription}"/>
    public string Assembly
    {
        get
        {
            if (_assembly is null)
            {
                _assembly = AssemblySymbol.Name;

                if (string.IsNullOrWhiteSpace(_assembly))
                {
                    throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(Assembly), "it was null, empty or all whitespaces");
                }
            }

            return _assembly;
        }
    }


    private string? _path;
    /// <exception cref="FailedToParseMetaDataException{NamespaceDescription}"/>
    public string Path
    {
        get
        {
            if (_path is null)
            {
                if (ContainingNamespace is not INamespaceSymbol namespaceSymbol)
                {
                    throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(Path), $"the '{nameof(ContainingNamespace)}' was not a '{nameof(INamespaceSymbol)}'");
                }

                _path = namespaceSymbol.ToString();

                if (string.IsNullOrWhiteSpace(_path))
                {
                    throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(Path), "it was null, empty or all whitespaces");
                }
            }

            return _path;
        }
    }


    private ISymbol? _containgingAssembly;
    private ISymbol ContaingingAssembly => _containgingAssembly ??= _typeNamedTypeSymbol.ContainingAssembly;

    private ISymbol? _containingNamespace;
    private ISymbol ContainingNamespace => _containingNamespace ??= _typeNamedTypeSymbol.ContainingNamespace;

    private IAssemblySymbol? _assemblySymbol;
    /// <exception cref="FailedToParseMetaDataException{NamespaceDescription}"/>
    private IAssemblySymbol AssemblySymbol
    {
        get
        {
            if (_assemblySymbol is null)
            {
                if (ContaingingAssembly is not IAssemblySymbol assemblySymbol)
                {
                    throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(AssemblySymbol), $"the '{nameof(ContaingingAssembly)}' was not a '{nameof(IAssemblySymbol)}'");
                }

                _assemblySymbol = assemblySymbol;
            }

            return _assemblySymbol;
        }
    }

    public override string ToString() => Path;
}
