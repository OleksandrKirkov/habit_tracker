using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Achievement
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserAcievement> UserAcievements { get; set; } = new List<UserAcievement>();
    }
}
