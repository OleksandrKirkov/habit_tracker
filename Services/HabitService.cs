using Core.DTO.Habits;
using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetHabitsByUserAsync(int userId);
    Task<Habit> GetByIdWithLogsAsync(int id);
    Task<Habit> CreateHabitAsync(CreateHabitRequest request, int UserId);
    Task ArchiveHabitAsync(int id);
    Task<bool> DeleteHabitAsync(int id);
    Task<bool> UpdateFrequencyAsync(int id, int frequency);
    Task<bool> UpdateReminderTimeAsync(int id, TimeSpan? reminderTime);
    Task<bool> UpdateReminderModeAsync(int id, string reminderMode);
    Task<bool> SetReminderStateAsync(int id, bool enabled);
}

public class HabitService : IHabitService
{
    private readonly IUnitOfWork _uow;

    public HabitService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Habit>> GetHabitsByUserAsync(int userId)
    {
        return await _uow.Habits.GetHabitsByUserAsync(userId);
    }

    public async Task<Habit> GetByIdWithLogsAsync(int id)
    {
        return await _uow.Habits.GetByIdWithLogsAsync(id);
    }

    public async Task<Habit> CreateHabitAsync(CreateHabitRequest request, int UserId)
    {
        var habit = new Habit
        {
            UserId = UserId,
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

    public async Task ArchiveHabitAsync(int id)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return;

        habit.IsArchived = true;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
    }

    public async Task<bool> DeleteHabitAsync(int id)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        _uow.Habits.Remove(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateFrequencyAsync(int id, int frequency)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.Frequency = frequency;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateReminderTimeAsync(int id, TimeSpan? reminderTime)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.ReminderTime = reminderTime;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateReminderModeAsync(int id, string reminderMode)
    {
        var habit = await _uow.Habits.GetByIdAsync(id);
        if (habit == null) return false;

        habit.ReminderMode = reminderMode;
        habit.UpdatedAt = DateTime.UtcNow;
        _uow.Habits.Update(habit);
        await _uow.CompleteAsync();
        return true;
    }

    public async Task<bool> SetReminderStateAsync(int id, bool enabled)
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
