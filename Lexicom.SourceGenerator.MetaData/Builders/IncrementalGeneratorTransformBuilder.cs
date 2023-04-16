using Lexicom.SourceGenerator.MetaData.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Lexicom.SourceGenerator.MetaData.Builders;
public interface IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNode> : IIncrementalGeneratorBuilder where TSyntaxNodePredicate : SyntaxNode where TSyntaxNode : SyntaxNode?
{
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<TSyntaxNode?, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<TSyntaxNode?, CancellationToken, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<GeneratorSyntaxContext, TSyntaxNode?, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<GeneratorSyntaxContext, TSyntaxNode?, CancellationToken, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode;

    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, ImmutableArray<TSyntaxNode>> generator);
    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, (Compilation Left, ImmutableArray<TSyntaxNode> Right)> generator);
}
public class IncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNode> : IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNode> where TSyntaxNodePredicate : SyntaxNode where TSyntaxNode : SyntaxNode?
{
    private readonly IncrementalGeneratorInitializationContext _context;
    private readonly Func<TSyntaxNodePredicate, CancellationToken, bool> _predicate;
    private readonly Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNode?> _transformDelegate;

    /// <exception cref="ArgumentNullException"/>
    public IncrementalGeneratorTransformBuilder(
        IncrementalGeneratorInitializationContext context,
        Func<TSyntaxNodePredicate, CancellationToken, bool> predicate,
        Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNode?> transformDelegate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        _context = context;
        _predicate = predicate;
        _transformDelegate = transformDelegate;
    }

    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<TSyntaxNode?, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((tsn, ct) => transformDelegate.Invoke(tsn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<TSyntaxNode?, CancellationToken, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((gsc, tsn, ct) => transformDelegate.Invoke(tsn, ct));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<GeneratorSyntaxContext, TSyntaxNode?, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((gsc, tsn, ct) => transformDelegate.Invoke(gsc, tsn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?> Transform<TSyntaxNodeTranform>(Func<GeneratorSyntaxContext, TSyntaxNode?, CancellationToken, TSyntaxNodeTranform?> transformDelegate) where TSyntaxNodeTranform : SyntaxNode
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorTransformBuilder<TSyntaxNodePredicate, TSyntaxNodeTranform?>(_context, _predicate, (gsc, ct) =>
        {
            TSyntaxNode? tsn = _transformDelegate.Invoke(gsc, ct);

            return transformDelegate.Invoke(gsc, tsn, ct);
        });
    }

    /// <exception cref="ArgumentNullException"/>
    public void Build(Action<SourceProductionContext, ImmutableArray<TSyntaxNode>> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        Build((spc, collectedSyntaxes) => generator.Invoke(spc, collectedSyntaxes.Right));
    }
    /// <exception cref="ArgumentNullException"/>
    public void Build(Action<SourceProductionContext, (Compilation Left, ImmutableArray<TSyntaxNode> Right)> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        _context.Build(_predicate.ToNormal(), _transformDelegate, generator);
    }
}
