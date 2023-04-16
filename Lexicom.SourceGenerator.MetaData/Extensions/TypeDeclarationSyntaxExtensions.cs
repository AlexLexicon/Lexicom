using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;

namespace Lexicom.SourceGenerator.MetaData.Extensions;
public static class TypeDeclarationSyntaxExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static string GetMetadataName(this TypeDeclarationSyntax sourceTypeSyntax)
    {
        if (sourceTypeSyntax is null)
        {
            throw new ArgumentNullException(nameof(sourceTypeSyntax));
        }

        var namespaces = new LinkedList<BaseNamespaceDeclarationSyntax>();
        var types = new LinkedList<TypeDeclarationSyntax>();
        for (SyntaxNode? parent = sourceTypeSyntax.Parent; parent is not null; parent = parent?.Parent)
        {
            if (parent is BaseNamespaceDeclarationSyntax namespaceSyntax)
            {
                namespaces.AddFirst(namespaceSyntax);
            }
            else if (parent is TypeDeclarationSyntax typeSyntax)
            {
                types.AddFirst(typeSyntax);
            }
        }

        var result = new StringBuilder();
        for (LinkedListNode<BaseNamespaceDeclarationSyntax> item = namespaces.First; item is not null; item = item.Next)
        {
            result
                .Append(item.Value.Name)
                .Append('.');
        }

        for (LinkedListNode<TypeDeclarationSyntax> item = types.First; item is not null; item = item.Next)
        {
            TypeDeclarationSyntax typeSyntax = item.Value;

            AppendTypeSyntaxName(result, typeSyntax);

            result.Append('+');
        }

        AppendTypeSyntaxName(result, sourceTypeSyntax);

        return result.ToString();
    }

    private static void AppendTypeSyntaxName(StringBuilder builder, TypeDeclarationSyntax type)
    {
        builder.Append(type.Identifier.Text);

        int typeArguments = type.TypeParameterList?
            .ChildNodes()
            .Count(node => node is TypeParameterSyntax) ?? 0;

        if (typeArguments > 0)
        {
            builder
                .Append('`')
                .Append(typeArguments);
        }
    }
}
