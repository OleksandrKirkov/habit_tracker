using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Habits
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        public string Color { get; set; } = "#000000";

        public string Icon { get; set; }

        [Required]
        [Range(1, 7)]
        public short Frequency { get; set; }

        [Required]
        [EnumDataType(typeof(HabitType))]
        public string Type { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? ReminderTime { get; set; }

        [EnumDataType(typeof(ReminderMode))]
        public string ReminderMode { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum HabitType
    {
        binary,
        duration,
        counter
    }

    public enum ReminderMode
    {
        once,
        daily,
        weekly
    }
}
