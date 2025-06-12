using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Habit> Habits { get; set; } = new List<Habit>();

        public ICollection<UserAcievement> UserAcievements { get; set; } = new List<UserAcievement>();

        public ICollection<SyncBackup> SyncBackups { get; set; } = new List<SyncBackup>();

        public ICollection<Integration> Integrations { get; set; } = new List<Integration>();
    }
}
