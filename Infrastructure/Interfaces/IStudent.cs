using Domain.Models;
namespace Infrastructure.Interfaces;

public interface IStudent
{
    public int AddStudent(Student student);
    public List<Student> GetAllStudent();
    public Student? GetStudentById(int studentId);
    public void UpdateStudent(Student student);
    public void DeleteStudent(int studentId);
    public List<Student> GetStudentsWithGroups();
    public List<Student> GetStudentsWithoutGroups();
    public List<Student> GetDroppedOutStudents();
    public List<Student> GetGraduatedStudents();
}
