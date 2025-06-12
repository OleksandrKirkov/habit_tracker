using Core.Models;

namespace Core.Interfaces;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetValidTokenAsync(string token);
    Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(int userId);
    Task<RefreshToken?> GetWithUserByTokenAsync(string token);
}
