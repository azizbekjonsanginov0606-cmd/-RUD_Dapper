using Domain.Models;
namespace Infrastructure.Interfaces;

public interface IStudentGroup
{
    public int AddStudentToGroup(StudentGroup studentGroup);
    public List<StudentGroup> GetStudentsByGroup(int groupId);
    public void UpdateStatus(int studentGroupId, StudentStatus newStatus);
    public void RemoveStudentFromGroup(int studentGroupId);
}
