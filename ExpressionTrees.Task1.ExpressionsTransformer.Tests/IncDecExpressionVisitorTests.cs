using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void VisitLambdaModified()
        {
            // Arrange
            var parameters = new Dictionary<string, object>
            {
                { "x", 3 },
                { "y", 5 }
            };

            Expression<Func<int, int, int>> expression = (x, y) => (2 + x) * y;

            // Act
            var visitor = new IncDecExpressionVisitor();
            var resultExpression = visitor.VisitLambdaModified(expression, parameters);

            // Assert
            Assert.IsNotNull(resultExpression);
        }
    }
}
