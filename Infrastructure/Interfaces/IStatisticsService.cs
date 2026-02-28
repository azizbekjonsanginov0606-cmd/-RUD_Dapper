using Domain.Models;
namespace Infrastructure.Interfaces;

public interface IStatisticsService
{
    public int GetTotalStudentsCount();
    public int GetTotalGroupsCount();
    public int GetTotalCoursesCount();
    public int GetTotalMentorsCount();
    public List<DateTime> GetAllStartDates();
} 
