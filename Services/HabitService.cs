using Core.Interfaces;
using Core.Models;

namespace Services;

public class HabitService
{
    private readonly IUnitOfWork _unitOfWork;

    public HabitService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Habit> CreateHabitAsync(int userId, string name, string description, string frequency)
    {
        var habit = new Habit
        {
            UserId = userId,
            Name = name,
            Description = description,
            Frequency = frequency
        };
        await _unitOfWork.Habits.AddAsync(habit);
        await _unitOfWork.CompleteAsync();

        return habit;
    }

    public async Task<Habit?> GetHabitWithCheckinsAsync(int habitId)
    {
        return await _unitOfWork.Habits.GetWithCheckinsAsync(habitId);
    }
}
