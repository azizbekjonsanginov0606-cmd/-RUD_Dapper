using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class GroupService:IGroup
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public int AddGroup(Group group)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                INSERT INTO Groups (GroupName, CourseId, MentorId, StartDate, EndDate)
                VALUES (@GroupName, @CourseId, @MentorId, @StartDate, @EndDate)
                RETURNING GroupId;";
            return connection.ExecuteScalar<int>(sql, group);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Add Group: {ex.Message}");
            return -1;
        }
    }

    public List<Group> GetAllGroup()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.Query<Group>("SELECT * FROM Groups ORDER BY StartDate;").ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetAll Groups: {ex.Message}");
            return new List<Group>();
        }
    }

    public Group? GetGroupById(int groupId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.QueryFirstOrDefault<Group>(
                "SELECT * FROM Groups WHERE GroupId = @GroupId;",
                new { GroupId = groupId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetById Group: {ex.Message}");
            return null;
        }
    }

    public void UpdateGroup(Group group)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                UPDATE Groups
                SET GroupName = @GroupName,
                    CourseId = @CourseId,
                    MentorId = @MentorId,
                    StartDate = @StartDate,
                    EndDate = @EndDate
                WHERE GroupId = @GroupId;";
            connection.Execute(sql, group);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Update Group: {ex.Message}");
        }
    }

    public void DeleteGroup(int groupId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sqlCheck = "SELECT COUNT(1) FROM StudentGroups WHERE GroupId = @GroupId;";
            var count = connection.ExecuteScalar<int>(sqlCheck, new { GroupId = groupId });

            if (count > 0)
            {
                Console.WriteLine("Ҳатогӣ: Гурӯҳ донишҷӯён дорад ва ҳазф карда намешавад.");
                return;
            }

            connection.Execute("DELETE FROM Groups WHERE GroupId = @GroupId;", new { GroupId = groupId });
            Console.WriteLine("Гурӯҳ ҳазф шуд");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Delete Group: {ex.Message}");
        }
    }
    public List<(int GroupId, int StudentCount)> GetStudentsPerGroup()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            var sql = @"
                SELECT GroupId, COUNT(*) AS StudentCount
                FROM StudentGroups
                GROUP BY GroupId;";
            return connection.Query(sql)
                             .Select(r => ((int)r.GroupId, (int)r.StudentCount))
                             .ToList();
        }

        public List<Group> GetEmptyGroups()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            var sql = @"
                SELECT g.* FROM Groups g
                LEFT JOIN StudentGroups sg ON g.GroupId = sg.GroupId
                WHERE sg.StudentGroupId IS NULL;";
            return connection.Query<Group>(sql).ToList();
        }
}
