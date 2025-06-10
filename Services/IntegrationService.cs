using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IIntegrationService
{
    Task<IEnumerable<Integration>> GetByUserAsync(Guid userId);
    Task<Integration> GetByProviderAsync(Guid userId, string provider);
    Task<Integration> CreateAsync(Integration integration);
}

public class IntegrationService : IIntegrationService
{
    private readonly IUnitOfWork _uow;

    public IntegrationService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Integration>> GetByUserAsync(Guid userId)
    {
        return await _uow.Integrations.GetByUserAsync(userId);
    }

    public async Task<Integration> GetByProviderAsync(Guid userId, string provider)
    {
        return await _uow.Integrations.GetByUserAndProviderAsync(userId, provider);
    }

    public async Task<Integration> CreateAsync(Integration integration)
    {
        await _uow.Integrations.AddAsync(integration);
        await _uow.CompleteAsync();
        return integration;
    }
}
