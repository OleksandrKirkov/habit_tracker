using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context) { }

    public async Task<RefreshToken?> GetValidTokenAsync(string token)
    {
        return await _context.RefreshToken
            .Include(rt => rt.User)
            .Where(rt => rt.Token == token && !rt.isRevoked && rt.ExpiresAt > DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(int userId)
    {
        return await _context.RefreshToken
            .Where(x => x.UserId == userId && !x.isRevoked)
            .ToListAsync();
    }

    public async Task<RefreshToken?> GetWithUserByTokenAsync(string token)
    {
        return await _context.RefreshToken
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == token && !x.isRevoked);
    }
}
