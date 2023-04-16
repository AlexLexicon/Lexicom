using Lexicom.SourceGenerator.MetaData.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData.Extensions;
public static class IncrementalGeneratorTransformBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorTransformBuilder<ClassDeclarationSyntax, ClassDeclarationSyntax?> HasAttribute<TAttribute>(this IIncrementalGeneratorTransformBuilder<ClassDeclarationSyntax, ClassDeclarationSyntax?> builder) where TAttribute : Attribute
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.Transform(GeneratorSyntaxContextExtenstions.HasAttributeTransform<TAttribute>);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorTransformBuilder<ClassDeclarationSyntax, ClassDeclarationSyntax?> HasInterface<TInterface>(this IIncrementalGeneratorTransformBuilder<ClassDeclarationSyntax, ClassDeclarationSyntax?> builder) where TInterface : class
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.Transform(GeneratorSyntaxContextExtenstions.HasInterfaceTransform<TInterface>);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void Build(this IIncrementalGeneratorTransformBuilder<ClassDeclarationSyntax, ClassDeclarationSyntax?> builder, Action<SourceProductionContext, IReadOnlyList<ClassMetaData>> generator)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Build((spc, ivp) => SourceProductionContextExtensions.BuildForMetaData(spc, ivp, generator));
    }
}
