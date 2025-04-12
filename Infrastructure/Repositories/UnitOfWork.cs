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
        HabitCheckins = new HabitCheckinRepository(_context);
    }

    public IUserRepository Users { get; }
    public IHabitRepository Habits { get; }
    public IHabitCheckinRepository HabitCheckins { get; }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
