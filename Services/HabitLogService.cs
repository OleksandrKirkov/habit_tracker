using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitLogService
{
    Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(Guid habitId);
    Task<HabitLog> LogAsync(Guid habitId, DateTime date, int? value);
    Task<bool> AlreadyLoggedAsync(Guid habitId, DateTime date);
}

public class HabitLogService : IHabitLogService
{
    private readonly IUnitOfWork _uow;

    public HabitLogService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<HabitLog>> GetLogsByHabitAsync(Guid habitId)
    {
        return await _uow.HabitLog.GetByHabitAsync(habitId);
    }

    public async Task<HabitLog> LogAsync(Guid habitId, DateTime date, int? value)
    {
        var existing = await _uow.HabitLog.GetByHabitAndDateAsync(habitId, date);
        if (existing != null) return existing;

        var log = new HabitLog
        {
            Id = Guid.NewGuid(),
            HabitId = habitId,
            LogDate = date.Date,
            Value = value,
            CreateAt = DateTime.UtcNow
        };

        await _uow.HabitLog.AddAsync(log);
        await _uow.CompleteAsync();

        return log;
    }

    public async Task<bool> AlreadyLoggedAsync(Guid habitId, DateTime date)
    {
        var existing = await _uow.HabitLog.GetByHabitAndDateAsync(habitId, date);
        return existing != null;
    }
}
