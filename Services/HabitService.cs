using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetHabitsByUserAsync(Guid userId);
    Task<Habit> GetByIdWithLogsAsync(Guid id);
    Task<Habit> CreateHabitAsync(Habit habit);
    Task ArchiveHabitAsync(Guid id);
}

public class HabitService : IHabitService
{
    private readonly IUnitOfWork _uow;

    public HabitService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Habit>> GetHabitsByUserAsync(Guid userId)
    {
        return await _uow.Habit.GetHabitsByUserAsync(userId);
    }

    public async Task<Habit> GetByIdWithLogsAsync(Guid id)
    {
        return await _uow.Habit.GetByIdWithLogsAsync(id);
    }

    public async Task<Habit> CreateHabitAsync(Habit habit)
    {
        await _uow.Habit.AddAsync(habit);
        await _uow.CompleteAsync();
        return habit;
    }

    public async Task ArchiveHabitAsync(Guid id)
    {
        var habit = await _uow.Habit.GetByIdAsync(id);
        if (habit == null) return;

        habit.IsArchived = true;
        _uow.Habit.Update(habit);
        await _uow.CompleteAsync();
    }
}
