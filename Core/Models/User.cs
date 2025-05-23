using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Habit> Habits { get; set; } = new List<Habit>();

        public ICollection<UserAcievement> UserAcievements { get; set; } = new List<UserAcievement>();

        public ICollection<SyncBackup> SyncBackups { get; set; } = new List<SyncBackup>();

        public ICollection<Integration> Integrations { get; set; } = new List<Integration>();
    }
}
