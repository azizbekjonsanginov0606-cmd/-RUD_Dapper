using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class CourseService : ICourse
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public int AddCourse(Course course)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                INSERT INTO Courses (Title, Description, DurationWeeks)
                VALUES (@Title, @Description, @DurationWeeks)
                RETURNING CourseId;";
            return connection.ExecuteScalar<int>(sql, course);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Add Course: {ex.Message}");
            return -1;
        }
    }

    public List<Course> GetAllCourse()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.Query<Course>("SELECT * FROM Courses ORDER BY Title;").ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetAll Courses: {ex.Message}");
            return new List<Course>();
        }
    }

    public Course? GetCourseById(int courseId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.QueryFirstOrDefault<Course>(
                "SELECT * FROM Courses WHERE CourseId = @CourseId;",
                new { CourseId = courseId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetById Course: {ex.Message}");
            return null;
        }
    }

    public void UpdateCourse(Course course)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                UPDATE Courses
                SET Title = @Title,
                    Description = @Description,
                    DurationWeeks = @DurationWeeks
                WHERE CourseId = @CourseId;";
            connection.Execute(sql, course);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Update Course: {ex.Message}");
        }
    }

    public void DeleteCourse(int courseId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sqlCheck = "SELECT COUNT(1) FROM Groups WHERE CourseId = @CourseId;";
            var count = connection.ExecuteScalar<int>(sqlCheck, new { CourseId = courseId });

            if (count > 0)
            {
                Console.WriteLine("Ҳатогӣ: Курс гурӯҳҳои вобаста дорад ва ҳазф карда намешавад.");
                return;
            }

            connection.Execute("DELETE FROM Courses WHERE CourseId = @CourseId;", new { CourseId = courseId });
            Console.WriteLine("Курс ҳазф шуд");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Delete Course: {ex.Message}");
        }
    }
    public List<(int CourseId, int StudentCount)> GetStudentsPerCourse()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT c.CourseId, COUNT(sg.StudentGroupId) AS StudentCount
                FROM Courses c
                LEFT JOIN Groups g ON c.CourseId = g.CourseId
                LEFT JOIN StudentGroups sg ON g.GroupId = sg.GroupId
                GROUP BY c.CourseId;";
        return connection.Query(sql)
                         .Select(r => ((int)r.CourseId, (int)r.StudentCount))
                         .ToList();
    }

    public Course? GetMostPopularCourse()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT c.*, COUNT(sg.StudentGroupId) AS StudentCount
                FROM Courses c
                JOIN Groups g ON c.CourseId = g.CourseId
                JOIN StudentGroups sg ON g.GroupId = sg.GroupId
                GROUP BY c.CourseId
                ORDER BY StudentCount DESC
                LIMIT 1;";
        return connection.QueryFirstOrDefault<Course>(sql);
    }
    public List<(int CourseId, string Title, int StudentCount)> GetTopThreeCourses()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var sql = @"
        SELECT c.CourseId, c.Title, COUNT(sg.StudentId) AS StudentCount
        FROM Courses c
        LEFT JOIN Groups g ON g.CourseId = c.CourseId
        LEFT JOIN StudentGroups sg ON sg.GroupId = g.GroupId
        GROUP BY c.CourseId, c.Title
        ORDER BY StudentCount DESC
        LIMIT 3;";

        return connection.Query<(int CourseId, string Title, int StudentCount)>(sql).ToList();
    }

    public List<(int CourseId, string Title, int StudentCount)> GetLeastPopularCourses()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var sql = @"
        SELECT c.CourseId, c.Title, COUNT(sg.StudentId) AS StudentCount
        FROM Courses c
        LEFT JOIN Groups g ON g.CourseId = c.CourseId
        LEFT JOIN StudentGroups sg ON sg.GroupId = g.GroupId
        GROUP BY c.CourseId, c.Title
        ORDER BY StudentCount ASC
        LIMIT 3;";

        return connection.Query<(int CourseId, string Title, int StudentCount)>(sql).ToList();
    }
}
