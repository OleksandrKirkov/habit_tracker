namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IHabitRepository Habits { get; }
    IHabitLogRepository HabitLogs { get; }

    IAchievementRepository Achievements { get; }
    IUserAchievementRepository UserAchievements { get; }
    ISyncBackupRepository SyncBackups { get; }
    IIntegrationRepository Integrations { get; }

    Task<int> CompleteAsync();
}
