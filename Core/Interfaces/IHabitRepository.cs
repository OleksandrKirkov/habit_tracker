using Core.Models;

namespace Core.Interfaces;

public interface IHabitRepository : IRepository<Habit>
{
    Task<IEnumerable<Habits>> GetHabitsByUserAsync(Guid userId);
    Task<Habit> GetByIdWithLogsAsync(Guid id);
}
