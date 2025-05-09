using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IntegrationRepository : Repository<Achievement>, IIntegrationRepository
    {
        public IntegrationRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Integration>> GetByUserAsync(Guid userId)
        {
            return await _context.Integrations
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<Integration> GetByUserAndProviderAsync(Guid userId, string provider)
        {
            return await _context.Integrations
                .FirstOrDefaultAsync(i => i.UserId == userId && i.Provider == provider);
        }
    }
}
