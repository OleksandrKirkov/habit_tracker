using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Habit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public string Color { get; set; } = "#000000";

        public string Icon { get; set; } = null!;

        [Required]
        [Range(1, 7)]
        public short Frequency { get; set; }

        [Required]
        [EnumDataType(typeof(HabitType))]
        public string Type { get; set; } = null!;

        [Column(TypeName = "time")]
        public TimeSpan? ReminderTime { get; set; }

        [EnumDataType(typeof(ReminderMode))]
        public string ReminderMode { get; set; } = null!;

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
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
