using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class StatisticsService:IStatisticsService
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public StatisticsService()
    {
    }

    public int GetTotalStudentsCount()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Students;");
        }

        public int GetTotalGroupsCount()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Groups;");
        }

        public int GetTotalCoursesCount()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Courses;");
        }

        public int GetTotalMentorsCount()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Mentors;");
        }

        public List<DateTime> GetAllStartDates()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection.Query<DateTime>("SELECT StartDate FROM Groups ORDER BY StartDate;").AsList();
        }
}
