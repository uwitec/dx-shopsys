using System;
using LinqExtender.Ast;
using LinqExtender;

namespace DBFramework
{
    public class EntityExpressionVisitor
    {
        internal Expression Visit(Expression expression)
        {
            switch (expression.CodeType)
            {
                case CodeType.BlockExpression:
                    return VisitBlockExpression((BlockExpression)expression);
                case CodeType.TypeExpression:
                    return VisitTypeExpression((TypeExpression)expression);
                case CodeType.LambdaExpresion:
                    return VisitLambdaExpression((LambdaExpression)expression);
                case CodeType.LogicalExpression:
                    return VisitLogicalExpression((LogicalExpression)expression);
                case CodeType.BinaryExpression:
                    return VisitBinaryExpression((BinaryExpression)expression);
                case CodeType.LiteralExpression:
                    return VisitLiteralExpression((LiteralExpression)expression);
                case CodeType.MemberExpression:
                    return VisitMemberExpression((MemberExpression)expression);
                case CodeType.OrderbyExpression:
                    return VisitOrderbyExpression((OrderbyExpression)expression);
                case CodeType.MethodCallExpression:
                    return VisitMethodCallExpression((MethodCallExpression)expression);
            }

            throw new ArgumentException("Expression type is not supported");
        }

        public virtual Expression VisitTypeExpression(TypeExpression typeExpression)
        {
            return typeExpression;
        }

        public virtual Expression VisitBlockExpression(BlockExpression blockExpression)
        {
            foreach (var expression in blockExpression.Expressions)
                this.Visit(expression);

            return blockExpression;
        }

        public virtual Expression VisitMethodCallExpression(MethodCallExpression methodCallExpression)
        {
            return methodCallExpression;
        }

        public virtual Expression VisitLogicalExpression(LogicalExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);
            return expression;
        }

        public virtual Expression VisitLambdaExpression(LambdaExpression expression)
        {
            if (expression.Body != null)
                return this.Visit(expression.Body);
            return expression;
        }

        public virtual Expression VisitBinaryExpression(BinaryExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);

            return expression;
        }

        public virtual Expression VisitMemberExpression(MemberExpression expression)
        {
            return expression;
        }

        public virtual Expression VisitLiteralExpression(LiteralExpression expression)
        {
            return expression;
        }

        public virtual Expression VisitOrderbyExpression(OrderbyExpression expression)
        {
            return expression;
        }

    }
}
