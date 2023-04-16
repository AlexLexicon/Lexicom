using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData;
public readonly record struct TypeMetaData
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FailedToParseMetaDataException{TypeDescription}"/>
    public TypeMetaData(INamedTypeSymbol namedTypeSymbol)
    {
        if (namedTypeSymbol is null)
        {
            throw new ArgumentNullException(nameof(namedTypeSymbol));
        }

        Name = namedTypeSymbol.Name;

        string? fullyQualifiedName = namedTypeSymbol.ToString();
        if (string.IsNullOrWhiteSpace(fullyQualifiedName))
        {
            throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(FullyQualifiedName), $"the '{nameof(namedTypeSymbol)}' was null, empty or all whitespaces");
        }
        FullyQualifiedName = fullyQualifiedName;

        var arguments = new List<TypeMetaData>();
        foreach (ITypeSymbol typeArgument in namedTypeSymbol.TypeArguments)
        {
            if (typeArgument is not INamedTypeSymbol typeArgumentSymbol)
            {
                throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(Arguments), $"the '{nameof(typeArgument)}' was not a {nameof(INamedTypeSymbol)}");
            }

            arguments.Add(new TypeMetaData(typeArgumentSymbol));
        }
        Arguments = arguments;
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FailedToParseMetaDataException{TypeDescription}"/>
    public TypeMetaData(IArrayTypeSymbol arrayTypeSymbol)
    {
        if (arrayTypeSymbol is null)
        {
            throw new ArgumentNullException(nameof(arrayTypeSymbol));
        }

        string? name = arrayTypeSymbol.ToString();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(Name), $"the '{nameof(name)}' was null, empty or all whitespaces");
        }
        Name = name;

        string? fullyQualifiedName = arrayTypeSymbol.ToString();
        if (string.IsNullOrWhiteSpace(fullyQualifiedName))
        {
            throw new FailedToParseMetaDataException<NamespaceMetaData>(nameof(FullyQualifiedName), $"the '{nameof(fullyQualifiedName)}' was null, empty or all whitespaces");
        }
        FullyQualifiedName = fullyQualifiedName;

        if (arrayTypeSymbol.ElementType is not INamedTypeSymbol elementTypeSymbol)
        {
            throw new FailedToParseMetaDataException<ParameterMetaData>(nameof(Arguments), $"the '{nameof(arrayTypeSymbol)}.{nameof(arrayTypeSymbol.ElementType)}' was not a '{nameof(INamedTypeSymbol)}'");
        }
        Arguments = new List<TypeMetaData>
        {
            new TypeMetaData(elementTypeSymbol),
        };
    }

    public string Name { get; }
    public string FullyQualifiedName { get; }
    public IReadOnlyList<TypeMetaData> Arguments { get; }

    public override string ToString() => FullyQualifiedName;
}
