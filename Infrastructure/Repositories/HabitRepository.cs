using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HabitRepository : Repository<Habit>, IHabitRepository
{
    public HabitRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Habit>> GetHabitsByUserAsync(int userId)
    {
        return await _context.Habits
            .Where(h => h.UserId == userId)
            .ToListAsync();
    }
    public async Task<Habit?> GetWithCheckinsAsync(int habitId)
    {
        return await _context.Habits
            .Include(h => h.Checkins)
            .FirstOrDefaultAsync(h => h.Id == habitId);
    }
}
