using Core.DTO.Habits;
using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetHabitsByUserAsync(Guid userId);
    Task<Habit> GetByIdWithLogsAsync(Guid id);
    Task<Habit> CreateHabitAsync(CreateHabitRequest request);
    Task ArchiveHabitAsync(Guid id);
    Task<bool> DeleteHabitAsync(Guid id);
    Task<bool> UpdateFrequencyAsync(Guid id, short frequency);
    Task<bool> UpdateReminderTimeAsync(Guid id, TimeSpan? reminderTime);
    Task<bool> UpdateReminderModeAsync(Guid id, string reminderMode);
    Task<bool> SetReminderStateAsync(Guid id, bool enabled);
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
        return await _uow.Habits.GetHabitsByUserAsync(userId);
    }

    public async Task<Habit> GetByIdWithLogsAsync(Guid id)
    {
        return await _uow.Habits.GetByIdWithLogsAsync(id);
    }

    public async Task<Habit> CreateHabitAsync(CreateHabitRequest request)
    {
        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Color = request.Color,
            Icon = request.Icon,
            Frequency = request.Frequency,
            Type = request.Type,
            ReminderTime = request.ReminderTime,
            ReminderMode = request.ReminderMode,
            CreatedAt = DateTime.UtcNow,
            IsArchived = false
        };

        await _uow.Habits.AddAsync(habit);
        await _uow.CompleteAsync();
        return habit;
    }

    public async Task ArchiveHabitAsync(Guid id)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return;

        habit.IsArchived = true;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
    }

    public async Task<bool> DeleteHabitAsync(Guid id)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        _uow.Habits.Remove(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateFrequencyAsync(Guid id, short frequency)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.Frequency = frequency;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateReminderTimeAsync(Guid id, TimeSpan? reminderTime)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.ReminderTime = reminderTime;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateReminderModeAsync(Guid id, string reminderMode)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.ReminderMode = reminderMode;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> SetReminderStateAsync(Guid id, bool enabled)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.ReminderMode = enabled ? "daily" : null;
        habit.ReminderTime = enabled ? habit.ReminderTime : null;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }
}
