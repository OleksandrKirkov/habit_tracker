using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HabitLogRepository : Repository<HabitLog>, IHabitLogRepository
{
    public HabitLogRepository(AppDbContext context) : base(context) { }

    public async Task<HabitLog?> GetByHabitAndDateAsync(Guid habitId, DateTime date)
    {
        return await _context.HabitLogs
            .FirstOrDefaultAsync(c => c.HabitId == habitId && c.LogDate == date.Date);
    }

    public async Task<IEnumerable<HabitLog>> GetByHabitAsync(Guid habitId)
    {
        return await _context.HabitLogs
            .Where(c => c.HabitId == habitId)
            .ToListAsync();
    }

    public async Task<IEnumerable<HabitLog>> GetByUserAsync(Guid userId)
    {
        return await _context.HabitLogs
            .Include(c => c.Habit)
            .Where(c => c.Habit.UserId == userId)
            .ToListAsync();
    }
}
