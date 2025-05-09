using Core.Models;

namespace Core.Interfaces;

public interface IHabitLogsRepository : IRepository<HabitLog>
{
    Task<HabitLog> GetByHabitAndDateAsync(Guid habitId, DateTime date);
    Task<IEnumerable<HabitLog>> GetByHabitAsync(Guid habitId);
    Task<IEnumerable<HabitLog>> GetByUserAsync(Guid userId);
}
