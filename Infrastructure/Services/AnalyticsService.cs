using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class AnalyticsService:IAnalyticsService
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public decimal GetCompletionRate()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT 
                    ROUND(
                        (COUNT(*) FILTER (WHERE Status = 3)::decimal / COUNT(*)::decimal) * 100, 2
                    ) AS CompletionRate
                FROM StudentGroups;";
        return connection.ExecuteScalar<decimal>(sql);
    }
}
