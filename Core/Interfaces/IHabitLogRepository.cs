using Core.Models;

namespace Core.Interfaces;

public interface IHabitLogRepository : IRepository<HabitLog>
{
    Task<HabitLog> GetByHabitAndDateAsync(int habitId, DateTime date);
    Task<IEnumerable<HabitLog>> GetByHabitAsync(int habitId);
    Task<IEnumerable<HabitLog>> GetByUserAsync(int userId);
}
