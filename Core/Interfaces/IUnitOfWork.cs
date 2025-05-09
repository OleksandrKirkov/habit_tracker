namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    IHabitRepository Habit { get; }
    IHabitLogRepository HabitLog { get; }

    IAchievementRepository Achievement { get; }
    IUserAchievementRepository UserAchievement { get; }
    ISyncBackupRepository SyncBackup { get; }
    IIntegrationRepository Integration { get; }

    Task<int> CompleteAsync();
}
