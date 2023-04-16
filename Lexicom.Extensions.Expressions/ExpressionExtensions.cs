using System.Diagnostics;
using System.Linq.Expressions;

namespace Lexicom.Extensions.Expressions;
public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>>? leftExpression, Expression<Func<T, bool>>? rightExpression)
    {
        if (leftExpression is null && rightExpression is null)
        {
            throw new ArgumentException($"One of the parameters '{nameof(leftExpression)}' or '{nameof(rightExpression)}' must not be null.");
        }

        if (leftExpression is null)
        {
            if (rightExpression is null)
            {
                throw new UnreachableException($"The parameter '{nameof(rightExpression)}' was null but cannot be at this point.");
            }

            return rightExpression;
        }

        if (rightExpression is null)
        {
            return leftExpression;
        }

        ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
        BinaryExpression bodyExpression = Expression.And(leftExpression.Body, rightExpression.Body);

        bodyExpression = (BinaryExpression)new ParameterReplacer(parameterExpression).Visit(bodyExpression);

        return Expression.Lambda<Func<T, bool>>(bodyExpression, parameterExpression);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>>? leftExpression, Expression<Func<T, bool>>? rightExpression)
    {
        if (leftExpression is null && rightExpression is null)
        {
            throw new ArgumentException($"One of the parameters '{nameof(leftExpression)}' or '{nameof(rightExpression)}' must not be null.");
        }

        if (leftExpression is null)
        {
            if (rightExpression is null)
            {
                throw new UnreachableException($"The parameter '{nameof(rightExpression)}' was null but cannot be at this point.");
            }

            return rightExpression;
        }

        if (rightExpression is null)
        {
            return leftExpression;
        }

        ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
        BinaryExpression bodyExpression = Expression.Or(leftExpression.Body, rightExpression.Body);

        bodyExpression = (BinaryExpression)new ParameterReplacer(parameterExpression).Visit(bodyExpression);

        return Expression.Lambda<Func<T, bool>>(bodyExpression, parameterExpression);
    }
}