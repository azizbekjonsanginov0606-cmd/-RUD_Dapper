using Domain.Models;
namespace Infrastructure.Interfaces;

public interface IGroup
{
    public int AddGroup(Group group);
    public List<Group> GetAllGroup();
    public Group? GetGroupById(int groupId);
    public void UpdateGroup(Group group);
    public void DeleteGroup(int groupId);
    public List<Group> GetEmptyGroups();
    public List<(int GroupId, int StudentCount)> GetStudentsPerGroup();
}
