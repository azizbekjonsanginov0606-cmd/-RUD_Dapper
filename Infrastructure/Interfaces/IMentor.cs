using Domain.Models;
namespace Infrastructure.Interfaces;

public interface IMentor
{
    public int AddMentor(Mentor mentor);
    public List<Mentor> GetAllMentor();
    public Mentor? GetMentorById(int mentorId);
    public void UpdateMentor(Mentor mentor);
    public void DeleteMentor(int mentorId);
    public Mentor? GetMentorWithMostStudents();
    public List<Mentor> GetMentorsWithMultipleCourses();
}
