using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AchievementRepository : Repository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context) : base(context) { }

        public async Task<Achievement?> GetByCodeAsync(string code)
        {
            return await _context.Achievements
                .FirstOrDefaultAsync(a => a.Code == code);
        }
    }
}
