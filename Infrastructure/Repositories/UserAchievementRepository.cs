using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserAchievementRepository : Repository<UserAcievement>, IUserAchievementRepository
    {
        public UserAchievementRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<UserAcievement>> GetByUserAsync(int userId)
        {
            return await _context.UserAcievements
                .Include(ua => ua.Achievement)
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int achievementId)
        {
            return await _context.UserAcievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
        }
    }
}
