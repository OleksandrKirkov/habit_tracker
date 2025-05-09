using Core.Models;

namespace Core.Interfaces;

public interface IIntegrationRepository : IRepository<Integration>
{
    Task<IEnumerable<Integration>> GetByUserAsync(Guid userId);
    Task<Integration> GetByUserAndProviderAsync(Guid userId, string provider);
}
