using Domain.Models;
namespace Infrastructure.Interfaces;

public interface ICourse
{
    public int AddCourse(Course course);
    public List<Course> GetAllCourse();
    public Course? GetCourseById(int courseId);
    public void UpdateCourse(Course course);
    public void DeleteCourse(int courseId);
    public List<(int CourseId, int StudentCount)> GetStudentsPerCourse();
    public Course? GetMostPopularCourse();
    public List<(int CourseId, string Title, int StudentCount)> GetTopThreeCourses();

    public List<(int CourseId, string Title, int StudentCount)> GetLeastPopularCourses();
}
