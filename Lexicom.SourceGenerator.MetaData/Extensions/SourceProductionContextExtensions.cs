using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;

namespace Lexicom.SourceGenerator.MetaData.Extensions;
public static class SourceProductionContextExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static string AddSource(this SourceProductionContext context, string className, StringBuilder sourceCode)
    {
        if (className is null)
        {
            throw new ArgumentNullException(nameof(className));
        }
        if (sourceCode is null)
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        string sourceCodeString = sourceCode.ToString();
        if (string.IsNullOrWhiteSpace(sourceCodeString))
        {
            throw new MetaDataSourceGeneratorException($"Cannot add source code becasue the '{nameof(sourceCode)}' string builder resulted in a string that was null, empty or whitespace");
        }

        context.AddSource($"{className}.g.cs", SourceText.From(sourceCodeString, Encoding.UTF8));

        return sourceCodeString;
    }

    internal static void BuildForMetaData(this SourceProductionContext sourceProductionContext, (Compilation Left, ImmutableArray<ClassDeclarationSyntax?> Right) ivp, Action<SourceProductionContext, IReadOnlyList<ClassMetaData>> generator)
    {
        var classDescriptions = new List<ClassMetaData>();

        foreach (ClassDeclarationSyntax? classDeclarationSyntax in ivp.Right)
        {
            if (classDeclarationSyntax is not null)
            {
                classDescriptions.Add(new ClassMetaData(ivp.Left, classDeclarationSyntax));
            }
        }

        generator.Invoke(sourceProductionContext, classDescriptions);
    }
}
