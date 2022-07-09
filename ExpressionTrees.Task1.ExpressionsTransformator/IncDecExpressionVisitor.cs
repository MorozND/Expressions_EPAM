using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        private Dictionary<string, ConstantExpression> _constantExpressions = new Dictionary<string, ConstantExpression>();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression resultExpression = null;

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    TryConvertBinaryExpression(node, ExpressionConvertTypes.Increment, out resultExpression);
                    break;

                case ExpressionType.Subtract:
                    TryConvertBinaryExpression(node, ExpressionConvertTypes.Decrement, out resultExpression);
                    break;
            }

            return resultExpression ?? base.VisitBinary(node);
        }

        public Expression VisitLambdaModified<T>(Expression<T> node, Dictionary<string, object> parameters)
        {
            _constantExpressions = parameters
                .ToDictionary(x => x.Key, x => Expression.Constant(x.Value));

            return Expression.Lambda(Visit(node.Body), node.Parameters);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _constantExpressions.TryGetValue(node.Name, out var constantExpression)
                ? (Expression)constantExpression
                : node;
        }

        private bool TryConvertBinaryExpression(BinaryExpression expression, ExpressionConvertTypes convertType, out Expression resultExpression)
        {
            Expression argumentExpression = null;
            ConstantExpression constantExpression = null;

            if (expression.Left.NodeType == ExpressionType.Constant)
            {
                constantExpression = expression.Left as ConstantExpression;
                argumentExpression = expression.Right;
            }
            else if (expression.Right.NodeType == ExpressionType.Constant)
            {
                constantExpression = expression.Right as ConstantExpression;
                argumentExpression = expression.Left;
            }
            else
            {
                resultExpression = null;
                return false;
            }

            if (unchecked((int)constantExpression.Value) == 1)
            {
                switch (convertType)
                {
                    case ExpressionConvertTypes.Increment:
                        resultExpression = Expression.Increment(argumentExpression);
                        return true;

                    case ExpressionConvertTypes.Decrement:
                        resultExpression = Expression.Decrement(argumentExpression);
                        return true;

                    default:
                        resultExpression = null;
                        return false;
                }
            }

            resultExpression = null;
            return false;
        }
    }
}
