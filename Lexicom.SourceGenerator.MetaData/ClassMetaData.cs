using Lexicom.SourceGenerator.MetaData.Exceptions;
using Lexicom.SourceGenerator.MetaData.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lexicom.SourceGenerator.MetaData;
public record struct ClassMetaData
{
    private readonly Compilation _compilation;
    private readonly ClassDeclarationSyntax _classDeclarationSyntax;

    /// <exception cref="ArgumentNullException"/>
    public ClassMetaData(
        Compilation compilation,
        ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (compilation is null)
        {
            throw new ArgumentNullException(nameof(compilation));
        }
        if (classDeclarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(classDeclarationSyntax));
        }

        _compilation = compilation;
        _classDeclarationSyntax = classDeclarationSyntax;

        _name = null;
        _fullyQualifiedName = null;
        _namespace = null;
        _baseClass = null;
        _attributes = null;
        _interfaces = null;
        _methods = null;
        _properties = null;
        _namedTypeSymbol = null;
    }

    private string? _name;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    public string Name
    {
        get
        {
            if (_name is null)
            {
                _name = NamedTypeSymbol.Name;

                if (string.IsNullOrWhiteSpace(_name))
                {
                    throw new FailedToParseMetaDataException<ClassMetaData>(nameof(Name), "it was null, empty or all whitespaces");
                }
            }

            return _name;
        }
    }

    private string? _fullyQualifiedName;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    public string FullyQualifiedName
    {
        get
        {
            if (_fullyQualifiedName is null)
            {
                _fullyQualifiedName = NamedTypeSymbol.ToString();

                if (string.IsNullOrWhiteSpace(_fullyQualifiedName))
                {
                    throw new FailedToParseMetaDataException<ClassMetaData>(nameof(FullyQualifiedName), "it was null, empty or all whitespaces");
                }
            }

            return _fullyQualifiedName;
        }
    }

    private NamespaceMetaData? _namespace;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    public NamespaceMetaData Namespace => _namespace ??= new NamespaceMetaData(NamedTypeSymbol);

    private BaseClassMetaData? _baseClass;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    public BaseClassMetaData BaseClass => _baseClass ??= new BaseClassMetaData(NamedTypeSymbol);

    private List<AttributeMetaData>? _attributes;
    public IReadOnlyList<AttributeMetaData> Attributes
    {
        get
        {
            if (_attributes is null)
            {
                _attributes = new List<AttributeMetaData>();

                foreach (AttributeListSyntax attributeListSyntax in _classDeclarationSyntax.AttributeLists)
                {
                    foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                    {
                        _attributes.Add(new AttributeMetaData(_compilation, attributeSyntax));
                    }
                }
            }

            return _attributes;
        }
    }

    private List<InterfaceMetaData>? _interfaces;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    public IReadOnlyList<InterfaceMetaData> Interfaces
    {
        get
        {
            if (_interfaces is null)
            {
                _interfaces = new List<InterfaceMetaData>();

                foreach (INamedTypeSymbol interfaceNamedTypeSymbol in NamedTypeSymbol.AllInterfaces)
                {
                    _interfaces.Add(new InterfaceMetaData(interfaceNamedTypeSymbol));
                }
            }

            return _interfaces;
        }
    }

    private List<MethodMetaData>? _methods;
    public IReadOnlyList<MethodMetaData> Methods
    {
        get
        {
            if (_methods is null)
            {
                _methods = new List<MethodMetaData>();

                foreach (MemberDeclarationSyntax memberSyntax in _classDeclarationSyntax.Members)
                {
                    if (memberSyntax is MethodDeclarationSyntax methodDeclarationSyntax)
                    {
                        _methods.Add(new MethodMetaData(_compilation, methodDeclarationSyntax));
                    }
                }
            }

            return _methods;
        }
    }

    private List<PropertyMetaData>? _properties;
    public IReadOnlyList<PropertyMetaData> Properties
    {
        get
        {
            if (_properties is null)
            {
                _properties = new List<PropertyMetaData>();

                foreach (MemberDeclarationSyntax memberSyntax in _classDeclarationSyntax.Members)
                {
                    if (memberSyntax is PropertyDeclarationSyntax propertyDeclarationSyntax)
                    {
                        _properties.Add(new PropertyMetaData(_compilation, propertyDeclarationSyntax));
                    }
                }
            }

            return _properties;
        }
    }

    private INamedTypeSymbol? _namedTypeSymbol;
    /// <exception cref="FailedToParseMetaDataException{ClassDescription}"/>
    private INamedTypeSymbol NamedTypeSymbol
    {
        get
        {
            if (_namedTypeSymbol is null)
            {
                _namedTypeSymbol = _compilation.GetTypeByMetadataName(_classDeclarationSyntax.GetMetadataName());

                if (_namedTypeSymbol is null)
                {
                    throw new FailedToParseMetaDataException<ClassMetaData>(nameof(NamedTypeSymbol), "it was null");
                }
            }
            return _namedTypeSymbol;
        }
    }

    public override string ToString() => FullyQualifiedName;
}
