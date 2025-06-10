using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IUserAchievementService
{
    Task<IEnumerable<UserAcievement>> GetByUserAsync(Guid userId);
    Task<UserAcievement> AssignAsync(Guid userId, Guid achievementId);
}

public class UserAchievementService : IUserAchievementService
{
    private readonly IUnitOfWork _uow;

    public UserAchievementService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<UserAcievement>> GetByUserAsync(Guid userId)
    {
        return await _uow.UserAchievements.GetByUserAsync(userId);
    }

    public async Task<UserAcievement> AssignAsync(Guid userId, Guid achievementId)
    {
        var exists = await _uow.UserAchievements.ExistsAsync(userId, achievementId);
        if (exists) return null!;

        var entity = new UserAcievement
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AchievementId = achievementId,
            UnlockedAt = DateTime.UtcNow
        };

        await _uow.UserAchievements.AddAsync(entity);
        await _uow.CompleteAsync();
        return entity;
    }
}
