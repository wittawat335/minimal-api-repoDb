using Dapper;
using System.Linq.Expressions;

namespace Infrastructure;

public static class ExpressionToSql
{
    public static string ToSql<T>(Expression<Func<T, bool>> expression, out DynamicParameters parameters)
    {
        parameters = new DynamicParameters();
        return Visit(expression.Body, parameters);
    }

    private static string Visit(Expression expr, DynamicParameters parameters)
    {
        if (expr is BinaryExpression be)
        {
            var left = Visit(be.Left, parameters);
            var right = Visit(be.Right, parameters);
            var op = ToSqlOperator(be.NodeType);
            return $"({left} {op} {right})";
        }

        if (expr is MemberExpression me && me.Expression?.NodeType == ExpressionType.Parameter)
        {
            return me.Member.Name; // property = column
        }

        if (expr is ConstantExpression ce)
        {
            var paramName = $"@p{parameters.ParameterNames.Count()}";
            parameters.Add(paramName, ce.Value);
            return paramName;
        }

        if (expr is MemberExpression me2 && me2.Expression is ConstantExpression ce2)
        {
            // external variable
            var obj = ce2.Value;
            var value = obj?.GetType().GetField(me2.Member.Name)?.GetValue(obj);
            var paramName = $"@p{parameters.ParameterNames.Count()}";
            parameters.Add(paramName, value);
            return paramName;
        }

        if (expr is MethodCallExpression mc)
        {
            return VisitMethodCall(mc, parameters);
        }

        throw new NotSupportedException($"Expression type {expr.NodeType} not supported");
    }

    private static string VisitMethodCall(MethodCallExpression mc, DynamicParameters parameters)
    {
        // string.Contains, StartsWith, EndsWith
        if (mc.Method.DeclaringType == typeof(string))
        {
            var member = Visit(mc.Object!, parameters);
            var arg = GetValue(mc.Arguments[0], parameters);

            var paramName = $"@p{parameters.ParameterNames.Count()}";

            return mc.Method.Name switch
            {
                nameof(string.Contains) =>
                    AddLikeParameter(parameters, paramName, $"%{arg}%", member),

                nameof(string.StartsWith) =>
                    AddLikeParameter(parameters, paramName, $"{arg}%", member),

                nameof(string.EndsWith) =>
                    AddLikeParameter(parameters, paramName, $"%{arg}", member),

                _ => throw new NotSupportedException($"Method {mc.Method.Name} not supported")
            };
        }

        // collection.Contains(x)
        if (mc.Method.Name == nameof(Enumerable.Contains))
        {
            var values = GetValue(mc.Arguments[0], parameters) as System.Collections.IEnumerable;
            var member = Visit(mc.Arguments[1], parameters);

            if (values == null)
                throw new NotSupportedException("Enumerable.Contains() requires a collection");

            var inParams = new List<string>();
            int i = parameters.ParameterNames.Count();

            foreach (var val in values)
            {
                var pname = $"@p{i++}";
                parameters.Add(pname, val);
                inParams.Add(pname);
            }

            return $"{member} IN ({string.Join(",", inParams)})";
        }

        throw new NotSupportedException($"Method {mc.Method.Name} not supported");
    }

    private static string AddLikeParameter(DynamicParameters parameters, string paramName, string value, string member)
    {
        parameters.Add(paramName, value);
        return $"{member} LIKE {paramName}";
    }

    private static object? GetValue(Expression expr, DynamicParameters parameters)
    {
        if (expr is ConstantExpression ce)
            return ce.Value;

        if (expr is MemberExpression me && me.Expression is ConstantExpression ce2)
        {
            var obj = ce2.Value;
            return obj?.GetType().GetField(me.Member.Name)?.GetValue(obj);
        }

        var lambda = Expression.Lambda(expr);
        return lambda.Compile().DynamicInvoke();
    }

    private static string ToSqlOperator(ExpressionType type) =>
        type switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "<>",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            ExpressionType.AndAlso => "AND",
            ExpressionType.OrElse => "OR",
            _ => throw new NotSupportedException($"Operator {type} not supported")
        };
}
