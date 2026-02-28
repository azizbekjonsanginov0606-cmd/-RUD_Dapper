using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class StudentGroupService : IStudentGroup
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public int AddStudentToGroup(StudentGroup studentGroup)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sqlCheck = @"
                SELECT COUNT(1) FROM StudentGroups 
                WHERE StudentId = @StudentId AND GroupId = @GroupId;";
            var exists = connection.ExecuteScalar<int>(sqlCheck, studentGroup);

            if (exists > 0)
            {
                Console.WriteLine("Ҳатогӣ: Донишҷӯ аллакай дар ин гурӯҳ ҳаст.");
                return -1;
            }

            var sql = @"
                INSERT INTO StudentGroups (StudentId, GroupId, Status)
                VALUES (@StudentId, @GroupId, @Status)
                RETURNING StudentGroupId;";
            return connection.ExecuteScalar<int>(sql, studentGroup);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ AddStudentToGroup: {ex.Message}");
            return -1;
        }
    }
    public List<StudentGroup> GetStudentsByGroup(int groupId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = "SELECT * FROM StudentGroups WHERE GroupId = @GroupId;";
            return connection.Query<StudentGroup>(sql, new { GroupId = groupId }).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetStudentsByGroup: {ex.Message}");
            return new List<StudentGroup>();
        }
    }

    public void UpdateStatus(int studentGroupId, StudentStatus newStatus)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
            UPDATE StudentGroups
            SET Status = @Status
            WHERE StudentGroupId = @StudentGroupId;";
            connection.Execute(sql, new { Status = (int)newStatus, StudentGroupId = studentGroupId });
            Console.WriteLine("Status навсозӣ шуд.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ UpdateStatus: {ex.Message}");
        }
    }

    public void RemoveStudentFromGroup(int studentGroupId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            connection.Execute("DELETE FROM StudentGroups WHERE StudentGroupId = @StudentGroupId;",
                new { StudentGroupId = studentGroupId });
            Console.WriteLine("Донишҷӯ аз гурӯҳ хориҷ шуд.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ RemoveStudentFromGroup: {ex.Message}");
        }
    }
}