using Core.Interfaces;
using Core.Models;

namespace Services;

public class HabitCheckinService
{
    private readonly IUnitOfWork _unitOfWork;

    public HabitCheckinService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HabitCheckin> CheckinAsync(int habitId)
    {
        var checkin = new HabitCheckin
        {
            HabitId = habitId,
            Date = DateTime.UtcNow.Date,
            Completed = true
        };

        await _unitOfWork.HabitCheckins.AddAsync(checkin);
        await _unitOfWork.CompleteAsync();
        return checkin;
    }

    public async Task<IEnumerable<HabitCheckin>> GetCheckinsAsync(int habitId)
    {
        var from = DateTime.UtcNow.Date.AddDays(-7);
        var to = DateTime.UtcNow.Date;
        return await _unitOfWork.HabitCheckins.GetCheckinsForHabitAsync(habitId, from, to);
    }
}
