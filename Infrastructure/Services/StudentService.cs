using Dapper;
using Npgsql;
using Domain.Models;
using Infrastructure.Interfaces;
namespace Infrastructure.Service;

public class StudentService : IStudent
{
    private const string ConnectionString = "Host=localhost;Port=5433;Database=crm;Username=hacker;Password=200606;";

    public int AddStudent(Student student)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                INSERT INTO Students (FullName, Email, Phone, EnrollmentDate)
                VALUES (@FullName, @Email, @Phone, @EnrollmentDate)
                RETURNING StudentId;";
            return connection.ExecuteScalar<int>(sql, student);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Add Student: {ex.Message}");
            return -1;
        }
    }

    public List<Student> GetAllStudent()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.Query<Student>("SELECT * FROM Students ORDER BY FullName;").ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetAll Students: {ex.Message}");
            return new List<Student>();
        }
    }

    public Student? GetStudentById(int studentId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            return connection.QueryFirstOrDefault<Student>(
                "SELECT * FROM Students WHERE StudentId = @StudentId;",
                new { StudentId = studentId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ GetById Student: {ex.Message}");
            return null;
        }
    }

    public void UpdateStudent(Student student)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sql = @"
                UPDATE Students
                SET FullName = @FullName,
                    Email = @Email,
                    Phone = @Phone,
                    EnrollmentDate = @EnrollmentDate
                WHERE StudentId = @StudentId;";
            connection.Execute(sql, student);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Update Student: {ex.Message}");
        }
    }

    public void DeleteStudent(int studentId)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var sqlCheck = "SELECT COUNT(1) FROM StudentGroups WHERE StudentId = @StudentId;";
            var count = connection.ExecuteScalar<int>(sqlCheck, new { StudentId = studentId });

            if (count > 0)
            {
                Console.WriteLine("Ҳатогӣ: Донишҷӯ дар гурӯҳ ҳаст ва ҳазф карда намешавад.");
                return;
            }

            connection.Execute("DELETE FROM Students WHERE StudentId = @StudentId;", new { StudentId = studentId });
            Console.WriteLine("Донишҷӯ ҳазф шуд");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ҳатогӣ Delete Student: {ex.Message}");
        }
    }
    
    public List<Student> GetStudentsWithGroups()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT s.* FROM Students s
                WHERE EXISTS (SELECT 1 FROM StudentGroups sg WHERE sg.StudentId = s.StudentId);";
        return connection.Query<Student>(sql).ToList();
    }



    public List<Student> GetStudentsWithoutGroups()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT s.* FROM Students s
                WHERE NOT EXISTS (SELECT 1 FROM StudentGroups sg WHERE sg.StudentId = s.StudentId);";
        return connection.Query<Student>(sql).ToList();
    }

    public List<Student> GetDroppedOutStudents()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT s.* FROM Students s
                JOIN StudentGroups sg ON s.StudentId = sg.StudentId
                WHERE sg.Status = 2;"; 
        return connection.Query<Student>(sql).ToList();
    }

    public List<Student> GetGraduatedStudents()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var sql = @"
                SELECT s.* FROM Students s
                JOIN StudentGroups sg ON s.StudentId = sg.StudentId
                WHERE sg.Status = 3;";
        return connection.Query<Student>(sql).ToList();
    }
}
