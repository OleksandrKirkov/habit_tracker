using Core.Models;

namespace Core.Interfaces;

public interface IAchievementRepository : IRepository<Achievement>
{
    Task<Achievement> GetByCodeAsync(string code);
}
