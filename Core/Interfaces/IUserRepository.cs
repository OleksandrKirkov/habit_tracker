using Core.Models;

namespace Core.Interfaces;

public interface IUserRepository : IRepository<Users>
{
    Task<Users> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
