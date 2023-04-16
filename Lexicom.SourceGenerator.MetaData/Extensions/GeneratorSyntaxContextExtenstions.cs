using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData.Extensions;
public static class GeneratorSyntaxContextExtenstions
{
    internal static ClassDeclarationSyntax? HasAttributeTransform<TAttribute>(this GeneratorSyntaxContext generatorSyntaxContext, ClassDeclarationSyntax? classDeclarationSyntax) where TAttribute : Attribute
    {
        if (classDeclarationSyntax is not null && classDeclarationSyntax.AttributeLists.Any())
        {
            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    ISymbol? attributeSymbolInfo = generatorSyntaxContext.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;

                    if (attributeSymbolInfo is null)
                    {
                        continue;
                    }

                    if (attributeSymbolInfo.ContainingType is null)
                    {
                        continue;
                    }

                    if (attributeSymbolInfo.ContainingType is not INamedTypeSymbol attributeNamedTypeSymbol)
                    {
                        continue;
                    }

                    string? attributeFullyQualifiedName = attributeNamedTypeSymbol.ToString();

                    if (attributeFullyQualifiedName == typeof(TAttribute).FullName)
                    {
                        return classDeclarationSyntax;
                    }
                }
            }
        }

        return null;
    }

    internal static ClassDeclarationSyntax? HasInterfaceTransform<TInterface>(this GeneratorSyntaxContext generatorSyntaxContext, ClassDeclarationSyntax? classDeclarationSyntax) where TInterface : class
    {
        if (classDeclarationSyntax is not null)
        {
            ISymbol? symbol = generatorSyntaxContext.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);

            if (symbol is not null && symbol is INamedTypeSymbol namedTypeSymbol && InheritsFromInterfaceRecursive(namedTypeSymbol))
            {
                return classDeclarationSyntax;
            }
        }

        return null;

        bool InheritsFromInterfaceRecursive(INamedTypeSymbol namedTypeSymbol)
        {
            if (namedTypeSymbol is null)
            {
                throw new ArgumentNullException(nameof(namedTypeSymbol));
            }

            if (namedTypeSymbol.Interfaces.Any())
            {
                foreach (INamedTypeSymbol namedInterfaceSymbol in namedTypeSymbol.Interfaces)
                {
                    string? interfaceFullyQualifiedName = namedInterfaceSymbol.ToString();

                    if (interfaceFullyQualifiedName == typeof(TInterface).FullName)
                    {
                        return true;
                    }
                }
            }

            if (namedTypeSymbol.BaseType is not null)
            {
                return InheritsFromInterfaceRecursive(namedTypeSymbol.BaseType);
            }

            return false;
        }
    }
}
