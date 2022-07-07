using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer.Tests
{
    [TestClass]
    public class IncDecExpressionVisitorTests
    {
        [TestMethod]
        public void VisitBinaryExpression_Increment()
        {
            // Arrange
            var variable = 2;

            Expression<Func<int>> expression = () => variable + 1;

            // Act
            var visitor = new IncDecExpressionVisitor();
            var resultExpression = visitor.Visit(expression);

            // Assert
            Assert.IsNotNull(resultExpression);
        }

        [TestMethod]
        public void VisitBinaryExpression_Decrement()
        {
            // Arrange
            var variable = 2;

            Expression<Func<int>> expression = () => variable - 1;

            // Act
            var visitor = new IncDecExpressionVisitor();
            var resultExpression = visitor.Visit(expression);

            // Assert
            Assert.IsNotNull(resultExpression);
        }
    }
}
