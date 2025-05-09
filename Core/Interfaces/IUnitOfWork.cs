namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IHabitRepository Habits { get; }
    IHabitLogsRepository HabitLogs { get; }

    IAchievementRepository Achievement { get; }
    IUserAchievementRepository UserAchievement { get; }
    ISyncBackupRepository SyncBackup { get; }
    IIntegrationRepository Integration { get; }

    Task<int> CompleteAsync();
}
