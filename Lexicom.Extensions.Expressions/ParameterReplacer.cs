using System.Linq.Expressions;

namespace Lexicom.Extensions.Expressions;
public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    /// <exception cref="ArgumentNullException"/>
    public ParameterReplacer(ParameterExpression parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return base.VisitParameter(_parameter);
    }
}
