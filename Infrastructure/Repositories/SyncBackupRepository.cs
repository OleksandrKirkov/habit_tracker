using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SyncBackupRepository : Repository<SyncBackup>, ISyncBackupRepository
    {
        public SyncBackupRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<SyncBackup>> GetByUserAsync(Guid userId)
        {
            return await _context.SyncBackups
                .Where(sb => sb.UserId == userId)
                .OrderByDescending(sb => sb.CreatedAt)
                .ToListAsync();
        }
    }
}
