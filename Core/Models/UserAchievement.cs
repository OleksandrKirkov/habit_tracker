using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class UserAcievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public int AchievementId { get; set; }

        [ForeignKey(nameof(AchievementId))]
        public Achievement Achievement { get; set; } = null!;

        public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
    }
}
