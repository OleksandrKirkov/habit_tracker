using Core.Models;

namespace Core.Interfaces;

public interface IHabitRepository : IRepository<Habit>
{
    Task<IEnumerable<Habit>> GetHabitsByUserAsync(int userId);
    Task<Habit?> GetWithCheckinsAsync(int habitId);
}
