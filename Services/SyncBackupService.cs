using Core.Interfaces;
using Core.Models;

namespace Services;

public interface ISyncBackupService
{
    Task<IEnumerable<SyncBackup>> GetByUserAsync(Guid userId);
    Task<SyncBackup> CreateAsync(SyncBackup backup);
}

public class SyncBackupService : ISyncBackupService
{
    private readonly IUnitOfWork _uow;

    public SyncBackupService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<SyncBackup>> GetByUserAsync(Guid userId)
    {
        return await _uow.SyncBackups.GetByUserAsync(userId);
    }

    public async Task<SyncBackup> CreateAsync(SyncBackup backup)
    {
        await _uow.SyncBackups.AddAsync(backup);
        await _uow.CompleteAsync();
        return backup;
    }
}
