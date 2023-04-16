using Lexicom.SourceGenerator.MetaData.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Lexicom.SourceGenerator.MetaData.Builders;
public interface IIncrementalGeneratorContextBuilder
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
public class IncrementalGeneratorContextBuilder : IIncrementalGeneratorContextBuilder
{
    private readonly IncrementalGeneratorInitializationContext _context;

    public IncrementalGeneratorContextBuilder(IncrementalGeneratorInitializationContext context)
    {
        _context = context;
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

        return new IncrementalGeneratorWhereBuilder(_context, predicate);
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

        return new IncrementalGeneratorWhereBuilder<TSyntaxNode>(_context, predicate);
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

        return new IncrementalGeneratorTransformBuilder<SyntaxNode, TSyntaxNode?>(_context, (sn, ct) => true, transformDelegate);
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

        _context.Build((sn, ct) => true, (gsc, ct) => gsc.Node, generator);
    }
}
