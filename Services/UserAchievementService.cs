using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IUserAchievementService
{
    Task<IEnumerable<UserAcievement>> GetByUserAsync(int userId);
    Task<UserAcievement> AssignAsync(int userId, int achievementId);
}

public class UserAchievementService : IUserAchievementService
{
    private readonly IUnitOfWork _uow;

    public UserAchievementService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<UserAcievement>> GetByUserAsync(int userId)
    {
        return await _uow.UserAchievements.GetByUserAsync(userId);
    }

    public async Task<UserAcievement> AssignAsync(int userId, int achievementId)
    {
        var exists = await _uow.UserAchievements.ExistsAsync(userId, achievementId);
        if (exists) return null!;

        var entity = new UserAcievement
        {
            UserId = userId,
            AchievementId = achievementId,
            UnlockedAt = DateTime.UtcNow
        };

        await _uow.UserAchievements.AddAsync(entity);
        await _uow.CompleteAsync();
        return entity;
    }
}
