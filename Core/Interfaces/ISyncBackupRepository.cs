using Core.Models;

namespace Core.Interfaces;

public interface ISyncBackupRepository : IRepository<SyncBackup>
{
    Task<IEnumerable<SyncBackup>> GetByUserAsync(int userId);
}
