using Core.Models;

namespace Core.Interfaces;

public interface IUserAchievementRepository : IRepository<UserAcievement>
{
    Task<IEnumerable<UserAcievement>> GetByUserAsync(Guid userId);
    Task<bool> ExistsAsync(Guid userId, Guid achievementId);
}
