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
        return await _uow.Integration.GetByUserAsync(userId);
    }

    public async Task<Integration> GetByProviderAsync(Guid userId, string provider)
    {
        return await _uow.Integration.GetByUserAndProviderAsync(userId, provider);
    }

    public async Task<Integration> CreateAsync(Integration integration)
    {
        await _uow.Integration.AddAsync(integration);
        await _uow.CompleteAsync();
        return integration;
    }
}
