using System.Linq.Expressions;
using System.Text;

namespace Infrastructure;

public static class OrderByBuilder<T>
{
    public static string ToSql(Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderByExpression)
    {
        if (orderByExpression.Body is MethodCallExpression mc)
        {
            return ParseMethod(mc);
        }

        throw new NotSupportedException("Unsupported OrderBy expression");
    }

    private static string ParseMethod(MethodCallExpression mc)
    {
        var sb = new StringBuilder();

        while (mc != null)
        {
            var methodName = mc.Method.Name; 
            var lambda = (LambdaExpression)((UnaryExpression)mc.Arguments[1]).Operand;
            var member = (lambda.Body as MemberExpression)?.Member.Name;

            if (string.IsNullOrEmpty(member))
                throw new NotSupportedException("Only simple property access is supported in OrderBy");

            if (sb.Length > 0)
                sb.Insert(0, ", ");

            sb.Insert(0, $"{member} {GetDirection(methodName)}");

            mc = mc.Arguments[0] as MethodCallExpression;
        }

        return sb.ToString();
    }

    private static string GetDirection(string methodName) =>
        methodName switch
        {
            "OrderBy" => "ASC",
            "ThenBy" => "ASC",
            "OrderByDescending" => "DESC",
            "ThenByDescending" => "DESC",
            _ => throw new NotSupportedException($"Method {methodName} not supported")
        };
}
