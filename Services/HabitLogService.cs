using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitLogService
{
    Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(int habitId, int userId);
    Task<HabitLog> LogAsync(int habitId, int userId, DateTime date, int? value);
    Task<bool> AlreadyLoggedAsync(int habitId, int userId, DateTime date);
}

public class HabitLogService : IHabitLogService
{
    private readonly IUnitOfWork _uow;

    public HabitLogService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(int habitId, int userId)
    {
        var habit = await _uow.Habits.GetByIdAsync(habitId);
        if (habit == null || habit.UserId != userId)
            return Enumerable.Empty<HabitLog>();

        return await _uow.HabitLogs.GetByHabitAsync(habitId);
    }

    public async Task<HabitLog> LogAsync(int habitId, int userId, DateTime date, int? value)
    {
        var habit = await _uow.Habits.GetByIdAsync(habitId);
        if (habit == null || habit.UserId != userId)
            throw new UnauthorizedAccessException("Habit does not belong to the user.");

        var existing = await _uow.HabitLogs.GetByHabitAndDateAsync(habitId, date);
        if (existing != null) return existing;

        var log = new HabitLog
        {
            HabitId = habitId,
            LogDate = date.Date,
            Value = value,
            CreateAt = DateTime.UtcNow
        };

        await _uow.HabitLogs.AddAsync(log);
        await _uow.CompleteAsync();

        return log;
    }

    public async Task<bool> AlreadyLoggedAsync(int habitId, int userId, DateTime date)
    {
        var habit = await _uow.Habits.GetByIdAsync(habitId);
        if (habit == null || habit.UserId != userId)
            return false;

        var existing = await _uow.HabitLogs.GetByHabitAndDateAsync(habitId, date);
        return existing != null;
    }
}
