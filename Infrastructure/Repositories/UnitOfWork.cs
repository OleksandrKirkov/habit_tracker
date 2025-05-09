using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Habits = new HabitRepository(_context);
        HabitLogs = new HabitLogRepository(_context);

        Achievements = new AchievementRepository(_context);
        UserAchievements = new UserAchievementRepository(_context);
        SyncBackups = new SyncBackupRepository(_context);
        Integrations = new IntegrationRepository(_context);
    }

    public IUserRepository Users { get; }
    public IHabitRepository Habits { get; }
    public IHabitLogRepository HabitLogs { get; }

    public IAchievementRepository Achievements { get; }
    public IUserAchievementRepository UserAchievements { get; }
    public ISyncBackupRepository SyncBackups { get; }
    public IIntegrationRepository Integrations { get; }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
