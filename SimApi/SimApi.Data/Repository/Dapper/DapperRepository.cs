using Dapper;
using SimApi.Base;
using SimApi.Data.Context;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace SimApi.Data.Repository;

public class DapperRepository<Entity> : IDapperRepository<Entity> where Entity : BaseModel
{
    protected readonly SimDapperDbContext context;
    private string tableName;
    private string schema;
    private bool disposed;

    public DapperRepository(SimDapperDbContext context)
    {
        TableAttribute? attribute = typeof(Entity).GetCustomAttribute<TableAttribute>();
        this.tableName = attribute?.Name ?? typeof(Entity).Name;
        this.schema = attribute?.Schema ?? "dbo";
        this.context = context;
    }
    public void DeleteById(int id)
    {
        var sqlAccount = $"DELETE FROM \"{schema}\".\"{tableName}\" WHERE \"Id\"=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            connection.Execute(sqlAccount, new { Id = id });
            connection.Close();
        }
    }

    public List<Entity> Filter(string sql)
    {
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Entity>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public List<Entity> GetAll()
    {
        var sql = $"SELECT * FROM \"{schema}\".\"{tableName}\"";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Entity>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public Entity GetById(int id)
    {
        var sql = $"SELECT * FROM \"{schema}\".\"{tableName}\" WHERE \"Id\"=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.QueryFirst<Entity>(sql, new { Id = id });
            connection.Close();
            return result;
        }
    }

    public void Insert(Entity entity)
    {
        var entityType = typeof(Entity);
        var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var columnNames = string.Join(",", properties.Select(p => $"\"{p.Name}\""));
        var valueNames = string.Join(",", properties.Select(p => $"@{p.Name}"));

        var sql = $"INSERT INTO \"{schema}\".\"{tableName}\" ({columnNames}) VALUES ({valueNames})";

        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = "sim@sim.com";

        var parameters = new DynamicParameters();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            parameters.Add(property.Name, value);
        }

        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Execute(sql, parameters);
        }
    }

    public void Update(Entity entity)
    {
        var entityType = typeof(Entity);
        var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var setStatements = string.Join(",", properties.Select(p => $"\"{p.Name}\" = @{p.Name}"));
        var sql = $"UPDATE \"{schema}\".\"{tableName}\" SET {setStatements} WHERE \"Id\" = @Id";

        var parameters = new DynamicParameters();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            parameters.Add(property.Name, value);
        }

        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Execute(sql, parameters);
        }
    }
}
