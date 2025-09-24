using Dapper;
using Domain.DBContext;
using Domain.Interfaces;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories;

public class DapperRepository<T> : IDapperRepository<T> where T : class
{
    private readonly DapperContext _context;
    private readonly string _tableName;

    public DapperRepository(DapperContext context)
    {
        _context = context;
        _tableName = typeof(T).Name; 
    }

    public async Task<int> DeleteAsync(object id)
    {
        using (var _connection = _context.CreateConnection())
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id=@Id";
            return await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? orderBy = null)
    {
        using (var _connection = _context.CreateConnection())
        {
            var whereClause = ExpressionToSql.ToSql(predicate, out var parameters);
            var sql = $"SELECT * FROM {_tableName} WHERE {whereClause}";

            if (orderBy != null)
            {
                var orderClause = OrderByBuilder<T>.ToSql(orderBy);
                sql += $" ORDER BY {orderClause}";
            }

            return await _connection.QueryAsync<T>(sql, parameters);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? orderBy = null)
    {
        using (var _connection = _context.CreateConnection())
        {
            var sql = $"SELECT * FROM {_tableName}";

            if (orderBy != null)
            {
                var orderClause = OrderByBuilder<T>.ToSql(orderBy);
                sql += $" ORDER BY {orderClause}";
            }

            return await _connection.QueryAsync<T>(sql);
        }
    }

    public async Task<IEnumerable<TParent>> GetAllWithIncludeAsync<TParent, TChild>(string childTable, string parentKey, string childKey, Func<TParent, TChild, TParent> map, string? orderBy = null)
    {
        using (var _connection = _context.CreateConnection())
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT p.*, c.* FROM {_tableName} p ");
            sql.Append($"INNER JOIN {childTable} c ON p.{parentKey} = c.{childKey}");

            if (!string.IsNullOrEmpty(orderBy))
                sql.Append($" ORDER BY {orderBy}");

            var result = await _connection.QueryAsync<TParent, TChild, TParent>(
                sql.ToString(),
                map,
                splitOn: "Id" 
            );

            return result;
        }
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        using (var _connection = _context.CreateConnection())
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }
    }

    public async Task<int> InsertAsync(T entity)
    {
        using (var _connection = _context.CreateConnection())
        {
            var props = typeof(T).GetProperties()
            .Where(p => p.Name != "Id"); 

            var columns = string.Join(",", props.Select(p => p.Name));
            var values = string.Join(",", props.Select(p => "@" + p.Name));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";

            return await _connection.ExecuteAsync(sql, entity);
        }
    }

    public async Task<int> UpdateAsync(T entity)
    {
        using (var _connection = _context.CreateConnection())
        {
            var props = typeof(T).GetProperties()
             .Where(p => p.Name != "Id");

            var setClause = string.Join(",", props.Select(p => $"{p.Name}=@{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause} WHERE Id=@Id";

            return await _connection.ExecuteAsync(sql, entity);
        }
    }
}
