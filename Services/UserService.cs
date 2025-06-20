using Core.Interfaces;
using Core.Models;

namespace Services;

public interface IUserService
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(int id);
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
        return await _uow.Users.GetByEmailAsync(email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _uow.Users.GetByIdAsync(id);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _uow.Users.EmailExistsAsync(email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _uow.Users.AddAsync(user);
        await _uow.CompleteAsync();
        return user;
    }
}
