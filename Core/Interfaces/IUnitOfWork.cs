namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IHabitRepository Habits { get; }
    IHabitCheckinRepository HabitCheckins { get; }

    Task<int> CompleteAsync();
}
