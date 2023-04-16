using Lexicom.SourceGenerator.MetaData.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Lexicom.SourceGenerator.MetaData.Builders;
public interface IIncrementalGeneratorWhereBuilder : IIncrementalGeneratorBuilder
{
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder Where(Func<SyntaxNode, bool> predicate);
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder Where(Func<SyntaxNode, CancellationToken, bool> predicate);
    IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>() where TSyntaxNode : SyntaxNode;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(Func<TSyntaxNode, bool> predicate) where TSyntaxNode : SyntaxNode;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(Func<TSyntaxNode, CancellationToken, bool> predicate) where TSyntaxNode : SyntaxNode;

    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(Func<GeneratorSyntaxContext, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode?;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode?;

    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, ImmutableArray<SyntaxNode>> generator);
    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, (Compilation Left, ImmutableArray<SyntaxNode> Right)> generator);
}
public class IncrementalGeneratorWhereBuilder : IIncrementalGeneratorWhereBuilder
{
    private readonly IncrementalGeneratorInitializationContext _context;
    private readonly Func<SyntaxNode, CancellationToken, bool> _predicate;

    /// <exception cref="ArgumentNullException"/>
    public IncrementalGeneratorWhereBuilder(
        IncrementalGeneratorInitializationContext context,
        Func<SyntaxNode, CancellationToken, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        _context = context;
        _predicate = predicate;
    }

    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder Where(Func<SyntaxNode, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return Where((sn, ct) => predicate.Invoke(sn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder Where(Func<SyntaxNode, CancellationToken, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorWhereBuilder(_context, (sn, ct) => _predicate.Invoke(sn, ct) && predicate.Invoke(sn, ct));
    }
    public IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>() where TSyntaxNode : SyntaxNode
    {
        return Where<TSyntaxNode>((sn, ct) => true);
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(Func<TSyntaxNode, bool> predicate) where TSyntaxNode : SyntaxNode
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return Where<TSyntaxNode>((sn, ct) => predicate.Invoke(sn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where<TSyntaxNode>(Func<TSyntaxNode, CancellationToken, bool> predicate) where TSyntaxNode : SyntaxNode
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorWhereBuilder<TSyntaxNode>(_context, (tsn, ct) => _predicate.Invoke(tsn, ct) && predicate.Invoke(tsn, ct));
    }

    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(Func<GeneratorSyntaxContext, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((gsc, ct) => transformDelegate.Invoke(gsc));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?> Transform<TSyntaxNode>(Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNode?> transformDelegate) where TSyntaxNode : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?>(_context, _predicate, transformDelegate);
    }

    /// <exception cref="ArgumentNullException"/>
    public void Build(Action<SourceProductionContext, ImmutableArray<SyntaxNode>> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        Build((spc, collectedSyntaxes) => generator.Invoke(spc, collectedSyntaxes.Right));
    }
    /// <exception cref="ArgumentNullException"/>
    public void Build(Action<SourceProductionContext, (Compilation Left, ImmutableArray<SyntaxNode> Right)> generator)
    {
        if (generator is null)
        {
            throw new ArgumentNullException(nameof(generator));
        }

        _context.Build(_predicate, (gsc, ct) => gsc.Node, generator);
    }
}
public interface IIncrementalGeneratorWhereBuilder<TSyntaxNode> : IIncrementalGeneratorBuilder where TSyntaxNode : SyntaxNode
{
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where(Func<TSyntaxNode, bool> predicate);
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where(Func<TSyntaxNode, CancellationToken, bool> predicate);

    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNode?, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?;
    /// <exception cref="ArgumentNullException"/>
    IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNode?, CancellationToken, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?;

    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, ImmutableArray<TSyntaxNode>> generator);
    /// <exception cref="ArgumentNullException"/>
    void Build(Action<SourceProductionContext, (Compilation Left, ImmutableArray<TSyntaxNode> Right)> generator);
}
public class IncrementalGeneratorWhereBuilder<TSyntaxNode> : IIncrementalGeneratorWhereBuilder<TSyntaxNode> where TSyntaxNode : SyntaxNode
{
    private readonly IncrementalGeneratorInitializationContext _context;
    private readonly Func<TSyntaxNode, CancellationToken, bool> _predicate;

    /// <exception cref="ArgumentNullException"/>
    public IncrementalGeneratorWhereBuilder(
        IncrementalGeneratorInitializationContext context,
        Func<TSyntaxNode, CancellationToken, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        _context = context;
        _predicate = predicate;
    }

    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where(Func<TSyntaxNode, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return Where((sn, ct) => predicate.Invoke(sn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorWhereBuilder<TSyntaxNode> Where(Func<TSyntaxNode, CancellationToken, bool> predicate)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return new IncrementalGeneratorWhereBuilder<TSyntaxNode>(_context, (tsn, ct) => _predicate.Invoke(tsn, ct) && predicate.Invoke(tsn, ct));
    }

    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((GeneratorSyntaxContext gsc, CancellationToken ct) => transformDelegate.Invoke(gsc));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, CancellationToken, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?>(_context, _predicate, transformDelegate);
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNode?, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return Transform((gsc, tsn, ct) => transformDelegate.Invoke(gsc, tsn));
    }
    /// <exception cref="ArgumentNullException"/>
    public IIncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?> Transform<TSyntaxNodeTransform>(Func<GeneratorSyntaxContext, TSyntaxNode?, CancellationToken, TSyntaxNodeTransform?> transformDelegate) where TSyntaxNodeTransform : SyntaxNode?
    {
        if (transformDelegate is null)
        {
            throw new ArgumentNullException(nameof(transformDelegate));
        }

        return new IncrementalGeneratorTransformBuilder<TSyntaxNode, TSyntaxNodeTransform?>(_context, _predicate, (gsc, ct) => transformDelegate.Invoke(gsc, (TSyntaxNode?)gsc.Node, ct));
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

        _context.Build(_predicate.ToNormal(), (gsc, ct) => (TSyntaxNode)gsc.Node, generator);
    }
}