using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
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
