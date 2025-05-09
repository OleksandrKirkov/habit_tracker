using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IUserService
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(Guid id);
    Task<bool> EmailExistsAsync(string email);
    Task<User> CreateUserAsync(User user);
}

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;

    public UserService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _uow.User.GetByEmailAsync(email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _uow.User.GetByIdAsync(id);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _uow.User.EmailExistsAsync(email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _uow.User.AddAsync(user);
        await _uow.CompleteAsync();
        return user;
    }
}
