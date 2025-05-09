using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<HabitLog> HabitLogs => Set<HabitLog>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<UserAcievement> UserAcievements => Set<UserAcievement>();
    public DbSet<SyncBackup> SyncBackups => Set<SyncBackup>();
    public DbSet<Integration> Integrations => Set<Integration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User -> Habits
        modelBuilder.Entity<User>()
            .HasMany(u => u.Habits)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Habit -> Checkins
        modelBuilder.Entity<Habit>()
            .HasMany(h => h.HabitLogs)
            .WithOne(c => c.Habit)
            .HasForeignKey(c => c.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> UserAchievements
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserAcievements)
            .WithOne(ua => ua.User)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Achievement -> UserAchievements
        modelBuilder.Entity<Achievement>()
            .HasMany(a => a.UserAcievements)
            .WithOne(ua => ua.Achievement)
            .HasForeignKey(ua => ua.AchievementId);

        // User -> SyncBackups
        modelBuilder.Entity<User>()
            .HasMany(u => u.SyncBackups)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Integrations
        modelBuilder.Entity<User>()
            .HasMany(u => u.Integrations)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unik Indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Achievement>()
            .HasIndex(a => a.Code)
            .IsUnique();

        modelBuilder.Entity<UserAcievement>()
            .HasIndex(ua => new { ua.UserId, ua.AchievementId })
            .IsUnique();

        modelBuilder.Entity<HabitLog>()
            .HasIndex(c => new { c.HabitId, c.LogDate })
            .IsUnique();
    }
}
