using Core.Models;

namespace Core.Interfaces;

public interface IIntegrationRepository : IRepository<Integration>
{
    Task<IEnumerable<Integration>> GetByUserAsync(int userId);
    Task<Integration> GetByUserAndProviderAsync(int userId, string provider);
}
