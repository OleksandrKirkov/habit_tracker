using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HabitCheckinRepository : Repository<HabitCheckin>, IHabitCheckinRepository
{
    public HabitCheckinRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<HabitCheckin>> GetCheckinsForHabitAsync(int habitId, DateTime from, DateTime to)
    {
        return await _context.HabitCheckins
            .Where(c => c.HabitId == habitId && c.Date >= from && c.Date <= to)
            .ToListAsync();
    }
}
