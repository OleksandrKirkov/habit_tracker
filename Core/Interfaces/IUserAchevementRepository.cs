using Core.Models;

namespace Core.Interfaces;

public interface IUserAchievementRepository : IRepository<UserAcievement>
{
    Task<IEnumerable<UserAcievement>> GetByUserAsync(int userId);
    Task<bool> ExistsAsync(int userId, int achievementId);
}
