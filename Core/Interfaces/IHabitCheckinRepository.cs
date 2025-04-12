using Core.Models;

namespace Core.Interfaces;

public interface IHabitCheckinRepository : IRepository<HabitCheckin>
{
    Task<IEnumerable<HabitCheckin>> GetCheckinsForHabitAsync(int habitId, DateTime from, DateTime to);
}
