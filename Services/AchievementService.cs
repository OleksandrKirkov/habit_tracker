using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IAchievementService
{
    Task<IEnumerable<Achievement>> GetAllAsync();
    Task<Achievement> GetByCodeAsync(string code);
    Task<Achievement> CreateAsync(Achievement achievement);
}

public class AchievementService : IAchievementService
{
    private readonly IUnitOfWork _uow;

    public AchievementService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Achievement>> GetAllAsync()
    {
        return await _uow.Achievement.GetAllAsync();
    }

    public async Task<Achievement> GetByCodeAsync(string code)
    {
        return await _uow.Achievement.GetByCodeAsync(code);
    }

    public async Task<Achievement> CreateAsync(Achievement achievement)
    {
        await _uow.Achievement.AddAsync(achievement);
        await _uow.CompleteAsync();
        return achievement;
    }
}
