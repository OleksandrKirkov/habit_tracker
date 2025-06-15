using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Core.DTO.Habits
{
    public class CreateHabitRequest
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public string Color { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public string Icon { get; set; } = null!;

        [Range(1, 7)]
        public int Frequency { get; set; }

        [EnumDataType(typeof(HabitType))]
        public string Type { get; set; } = null!;

        public TimeSpan? ReminderTime { get; set; }

        [EnumDataType(typeof(ReminderMode))]
        public string ReminderMode { get; set; } = null!;
    }

    public class GetHabitsByUser
    {
        public List<Habit> Habits { get; set; } = new();
    }
}
