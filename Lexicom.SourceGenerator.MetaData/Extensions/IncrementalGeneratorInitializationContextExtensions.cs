using Lexicom.SourceGenerator.MetaData.Builders;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Lexicom.SourceGenerator.MetaData.Extensions;
public static class IncrementalGeneratorInitializationContextExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorWhereBuilder Where(this IncrementalGeneratorInitializationContext context, Func<SyntaxNode, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorContextBuilder(context).Where(predicate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorWhereBuilder Where(this IncrementalGeneratorInitializationContext context, Func<SyntaxNode, CancellationToken, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorContextBuilder(context).Where(predicate);
    }
    public static IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(this IncrementalGeneratorInitializationContext context) where TSyntaxNode : SyntaxNode
    {
        return new IncrementalGeneratorContextBuilder(context).Where<TSyntaxNode>();
    }
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(this IncrementalGeneratorInitializationContext context, Func<TSyntaxNode, bool> predicate) where TSyntaxNode : SyntaxNode
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorContextBuilder(context).Where(predicate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(this IncrementalGeneratorInitializationContext context, Func<TSyntaxNode, CancellationToken, bool> predicate) where TSyntaxNode : SyntaxNode
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorContextBuilder(context).Where(predicate);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(this IncrementalGeneratorInitializationContext context, Func<GeneratorSyntaxContext, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorContextBuilder(context).Transform(transformDelegate);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(this IncrementalGeneratorInitializationContext context, Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorContextBuilder(context).Transform(transformDelegate);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void Build(this IncrementalGeneratorInitializationContext context, Action<SourceProductionContext, ImmutableArray<SyntaxNode>> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        new IncrementalGeneratorContextBuilder(context).Build(generator);
    }
    /// <exception cref="ArgumentNullException"/>
    public static void Build(this IncrementalGeneratorInitializationContext context, Action<SourceProductionContext, (Compilation Left, ImmutableArray<SyntaxNode> Right)> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        new IncrementalGeneratorContextBuilder(context).Build(generator);
    }

    internal static void Build<T>(
        this IncrementalGeneratorInitializationContext context,
        Func<SyntaxNode, CancellationToken, bool> predicate,
        Func<GeneratorSyntaxContext, CancellationToken, T?> transform,
        Action<SourceProductionContext, (Compilation Left, ImmutableArray<T> Right)> generator)
    {
        IncrementalValuesProvider<T> syntaxesProvider = context.SyntaxProvider
            .CreateSyntaxProvider(predicate, transform)
            .Where(sn => sn is not null)!;

        IncrementalValueProvider<ImmutableArray<T>> syntaxesArray = syntaxesProvider.Collect();

        IncrementalValueProvider<(Compilation Left, ImmutableArray<T> Right)> collectedSyntaxes = context.CompilationProvider.Combine(syntaxesArray);

        context.RegisterSourceOutput(collectedSyntaxes, generator);
    }

    internal static Func<SyntaxNode, CancellationToken, bool> ToNormal<TSyntaxNode>(this Func<TSyntaxNode, CancellationToken, bool> predicate)
    {
        return (sn, ct) =>
        {
            if (sn is TSyntaxNode tsn)
            {
                return predicate.Invoke(tsn, ct);
            }

            return false;
        };
    }
}
