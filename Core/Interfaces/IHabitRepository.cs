using Core.Models;

namespace Core.Interfaces;

public interface IHabitRepository : IRepository<Habit>
{
    Task<IEnumerable<Habit>> GetHabitsByUserAsync(int userId);
    Task<Habit> GetByIdWithLogsAsync(int id);
}
