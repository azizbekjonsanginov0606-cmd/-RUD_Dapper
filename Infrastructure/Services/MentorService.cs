using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Service;

public class MentorService : IMentor
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public int AddMentor(Mentor mentor)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                    INSERT INTO Mentors (FullName, Email, Phone, Specialization)
                    VALUES (@FullName, @Email, @Phone, @Specialization)
                    RETURNING MentorId;";
            return connection.ExecuteScalar<int>(sql, mentor);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Add Mentor: {ex.Message}");
            return -1;
        }
    }

    public List<Mentor> GetAllMentor()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = "SELECT * FROM Mentors ORDER BY FullName;";
            return connection.Query<Mentor>(sql).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetAll Mentors: {ex.Message}");
            return new List<Mentor>();
        }
    }

    public Mentor? GetMentorById(int mentorId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = "SELECT * FROM Mentors WHERE MentorId = @MentorId;";
            return connection.QueryFirstOrDefault<Mentor>(sql, new { MentorId = mentorId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetMentorById: {ex.Message}");
            return null;
        }
    }

    public void UpdateMentor(Mentor mentor)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                    UPDATE Mentors
                    SET FullName = @FullName,
                        Email = @Email,
                        Phone = @Phone,
                        Specialization = @Specialization
                    WHERE MentorId = @MentorId;";
            connection.Execute(sql, mentor);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Update Mentor: {ex.Message}");
        }
    }

    public void DeleteMentor(int mentorId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sqlCheck = "SELECT COUNT(1) FROM Groups WHERE MentorId = @MentorId;";
            var count = connection.ExecuteScalar<int>(sqlCheck, new { MentorId = mentorId });

            if (count > 0)
            {
                Console.WriteLine("Ҳатогӣ: Мураббӣ гурӯҳҳои вобаста дорад ва ҳазф карда намешавад.");
                return;
            }

            var sqlDelete = "DELETE FROM Mentors WHERE MentorId = @MentorId;";
            connection.Execute(sqlDelete, new { MentorId = mentorId });
            Console.WriteLine("Мураббӣ ҳазф шуд");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Delete Mentor: {ex.Message}");
        }
    }
    public Mentor? GetMentorWithMostStudents()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT m.*, COUNT(sg.StudentGroupId) AS StudentCount
                FROM Mentors m
                JOIN Groups g ON m.MentorId = g.MentorId
                JOIN StudentGroups sg ON g.GroupId = sg.GroupId
                GROUP BY m.MentorId
                ORDER BY StudentCount DESC
                LIMIT 1;";
        return connection.QueryFirstOrDefault<Mentor>(sql);
    }

    public List<Mentor> GetMentorsWithMultipleCourses()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT m.*
                FROM Mentors m
                JOIN Groups g ON m.MentorId = g.MentorId
                GROUP BY m.MentorId
                HAVING COUNT(DISTINCT g.CourseId) > 1;";
        return connection.Query<Mentor>(sql).ToList();
    }
}