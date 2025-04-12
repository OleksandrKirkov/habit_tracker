using Core.Interfaces;
using Core.Models;

namespace Services;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> CreateUserAsync(string username)
    {
        var user = new User { Username = username };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<IEnumerable<Habit>> GetUserHabitsAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        return user?.Habits ?? new List<Habit>();
    }
}
