using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LinqExtender;
using LinqExtender.Ast;
using DBFramework.Entities;

namespace DBFramework
{
    public class EntityContext<T> : EntityExpressionVisitor, IQueryContext<T> where T : IEntity, new()
    {
        private IQueryable Query;

        public StringBuilder Writer = new StringBuilder();

        public IEnumerable<T> Execute(Expression expression)
        {
            this.Visit(expression);
            return CollectionEntities();
        }

        public void Execute(IQueryable query)
        {
            Query = query;
            (query as LinqExtender.QueryProvider<T>).Execute((query as LinqExtender.QueryProvider<T>).Expression);
        }

        public override Expression VisitTypeExpression(TypeExpression expression)
        {
            ClearWrite();
            Writer.Append(string.Format("SELECT * FROM {0}", expression.Type.Name));
            return expression;
        }

        public override Expression VisitLambdaExpression(LambdaExpression expression)
        {
            WriteSpace();
            Writer.Append("WHERE");
            WriteSpace();

            this.Visit(expression.Body);

            return expression;
        }

        public override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            this.Visit(expression.Left);
            Writer.Append(GetBinaryOperator(expression.Operator));
            this.Visit(expression.Right);

            return expression;
        }

        public override Expression VisitLogicalExpression(LogicalExpression expression)
        {
            WriteTokenIfReq(expression, Token.LeftParenthesis);

            this.Visit(expression.Left);

            WriteLogicalOperator(expression.Operator);

            this.Visit(expression.Right);

            WriteTokenIfReq(expression, Token.RightParentThesis);

            return expression;
        }

        public override Expression VisitMemberExpression(MemberExpression expression)
        {
            Writer.Append(expression.FullName);
            return expression;
        }

        public override Expression VisitLiteralExpression(LiteralExpression expression)
        {
            WriteValue(expression.Type, expression.Value);
            return expression;
        }

        public override Expression VisitOrderbyExpression(OrderbyExpression expression)
        {
            WriteSpace();
            Write(string.Format("ORDER BY {0}.{1} {2}",
                expression.Member.DeclaringType.Name,
                expression.Member.Name,
                expression.Ascending ? "ASC" : "DESC"));
            WriteSpace();

            return expression;
        }

        private static string GetBinaryOperator(BinaryOperator @operator)
        {
            switch (@operator)
            {
                case BinaryOperator.GreaterThan:
                    return " > ";
                case BinaryOperator.LessThan:
                    return " < ";
                case BinaryOperator.GreaterThanEqual:
                    return " >= ";
                case BinaryOperator.LessThanEqual:
                    return " <= ";
                case BinaryOperator.NotEqual:
                    return " != ";
                case BinaryOperator.Equal:
                    return " = ";
                default:
                    break;
            }
            throw new ArgumentException("Invalid binary operator");
        }

        private void WriteLogicalOperator(LogicalOperator logicalOperator)
        {
            WriteSpace();

            Writer.Append(logicalOperator.ToString().ToUpper());

            WriteSpace();
        }

        private void WriteSpace()
        {
            Writer.Append(" ");
        }

        private void WriteTokenIfReq(LogicalExpression expression, Token token)
        {
            if (expression.IsChild)
            {
                WriteToken(token);
            }
        }

        private void WriteToken(Token token)
        {
            switch (token)
            {
                case Token.LeftParenthesis:
                    Writer.Append("(");
                    break;
                case Token.RightParentThesis:
                    Writer.Append(")");
                    break;
            }
        }

        public enum Token
        {
            LeftParenthesis,
            RightParentThesis
        }

        private void WriteValue(TypeReference type, object value)
        {
            if (value == null)
            {
                Writer.Append(String.Format("{0}", "NULL"));
                return;
            }
            if (type.UnderlyingType == typeof(string))
            {
                Writer.Append(String.Format("\'{0}\'", value));
            }
            else if (type.UnderlyingType == typeof(DateTime?) || type.UnderlyingType == typeof(DateTime))
            {
                Writer.Append(String.Format("\'{0}\'", value));
            }
            else if (type.UnderlyingType == typeof(bool?) || type.UnderlyingType == typeof(bool))
            {
                Writer.Append(String.Format("{0}", (bool)value? 1:0));
            }
            else
                Writer.Append(value);
        }

        private void Write(string value)
        {
            Writer.Append(value);
        }

        private void ClearWrite()
        {
            Writer.Clear();
        }

        private IEnumerable<T> CollectionEntities()
        {
            Query = null;
            if (Writer.ToString() != "" && SQLHelper.IsSetup)
                return SQLHelper.GetEntitiesByQuery<T>(Writer.ToString());
            return new List<T>().AsEnumerable();
        }

    }
}
