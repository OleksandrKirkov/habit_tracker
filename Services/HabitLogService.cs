using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitLogService
{
    Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(int habitId);
    Task<HabitLog> LogAsync(int habitId, DateTime date, int? value);
    Task<bool> AlreadyLoggedAsync(int habitId, DateTime date);
}

public class HabitLogService : IHabitLogService
{
    private readonly IUnitOfWork _uow;

    public HabitLogService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(int habitId)
    {
        return await _uow.HabitLogs.GetByHabitAsync(habitId);
    }

    public async Task<HabitLog> LogAsync(int habitId, DateTime date, int? value)
    {
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

    public async Task<bool> AlreadyLoggedAsync(int habitId, DateTime date)
    {
        var existing = await _uow.HabitLogs.GetByHabitAndDateAsync(habitId, date);
        return existing != null;
    }
}
