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

    public class UpdateHabitFrequencyRequest
    {
        public int Frequency { get; set; }
    }

    public class UpdateReminderTime
    {
        public TimeSpan Time { get; set; }
    }

    public class UpdateReminderMode
    {
        public string Mode { get; set; } = null!;
    }

    public class UpdateReminderState
    {
        public bool Enabled { get; set; }
    }
    public class HabitDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Color { get; set; } = "#000000";
        public string Icon { get; set; } = null!;
        public int Frequency { get; set; }
        public string Type { get; set; } = null!;
        public TimeSpan? ReminderTime { get; set; }
        public string ReminderMode { get; set; } = null!;
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<HabitLogDto> HabitLogs { get; set; } = new();
    }

    public class HabitLogDto
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public int? Value { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public static class HabitMapper
    {
        public static HabitDto ToDto(Habit habit)
        {
            return new HabitDto
            {
                Id = habit.Id,
                Title = habit.Title,
                Color = habit.Color,
                Icon = habit.Icon,
                Frequency = habit.Frequency,
                Type = habit.Type,
                ReminderTime = habit.ReminderTime,
                ReminderMode = habit.ReminderMode,
                IsArchived = habit.IsArchived,
                CreatedAt = habit.CreatedAt,
                UpdatedAt = habit.UpdatedAt,
                HabitLogs = habit.HabitLogs?.Select(log => new HabitLogDto
                {
                    Id = log.Id,
                    LogDate = log.LogDate,
                    Value = log.Value,
                    CreateAt = log.CreateAt
                }).ToList() ?? new()
            };
        }
    }
}
